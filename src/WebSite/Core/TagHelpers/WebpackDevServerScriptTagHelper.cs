// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Razor.TagHelpers;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.TagHelpers;
using Microsoft.AspNet.Mvc.TagHelpers.Internal;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Microsoft.Extensions.OptionsModel;

namespace WebSite.Core.TagHelpers
{
    using Config;

    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;script&gt; elements that supports webpack-dev-server.
    /// </summary>
    /// <remarks>
    /// The tag helper won't process for cases with just the 'src' attribute.
    /// </remarks>
    [HtmlTargetElement("script", Attributes = SrcIncludeAttributeName)]
    public class WebpackDevServerScriptTagHelper : UrlResolutionTagHelper
    {
        private const string SrcIncludeAttributeName = "webpack-dev-server-src-include";
        private const string SrcAttributeName = "src";

        private DevelopmentSettings devSettings;

        /// <summary>
        /// Creates a new <see cref="WebpackDevServerScriptTagHelper"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{ScriptTagHelper}"/>.</param>
        /// <param name="hostingEnvironment">The <see cref="IHostingEnvironment"/>.</param>
        /// <param name="cache">The <see cref="IMemoryCache"/>.</param>
        /// <param name="htmlEncoder">The <see cref="IHtmlEncoder"/>.</param>
        /// <param name="javaScriptEncoder">The <see cref="IJavaScriptStringEncoder"/>.</param>
        /// <param name="urlHelper">The <see cref="IUrlHelper"/>.</param>
        public WebpackDevServerScriptTagHelper(
            ILogger<WebpackDevServerScriptTagHelper> logger,
            IHostingEnvironment hostingEnvironment,
            IMemoryCache cache,
            IHtmlEncoder htmlEncoder,
            IJavaScriptStringEncoder javaScriptEncoder,
            IUrlHelper urlHelper,
            IOptions<DevelopmentSettings> options)
            : base(urlHelper, htmlEncoder)
        {
            Logger = logger;
            HostingEnvironment = hostingEnvironment;
            Cache = cache;
            JavaScriptEncoder = javaScriptEncoder;
            devSettings = options.Value;
        }

        /// <inheritdoc />
        public override int Order
        {
            get
            {
                return -1000;
            }
        }

        /// <summary>
        /// Address of the external script to use.
        /// </summary>
        /// <remarks>
        /// Passed through to the generated HTML in all cases.
        /// </remarks>
        [HtmlAttributeName(SrcAttributeName)]
        public string Src { get; set; }

        /// <summary>
        /// A comma separated list of globbed file patterns of JavaScript scripts to load.
        /// The glob patterns are assessed relative to the application's 'webroot' setting.
        /// </summary>
        [HtmlAttributeName(SrcIncludeAttributeName)]
        public string SrcInclude { get; set; }

        protected ILogger<WebpackDevServerScriptTagHelper> Logger { get; }

        protected IHostingEnvironment HostingEnvironment { get; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IMemoryCache Cache { get; }

        protected IJavaScriptStringEncoder JavaScriptEncoder { get; }

        // Internal for ease of use when testing.
        protected internal GlobbingUrlBuilder GlobbingUrlBuilder { get; set; }

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            // Pass through attribute that is also a well-known HTML attribute.
            if (Src != null)
            {
                output.CopyHtmlAttribute(SrcAttributeName, context);
            }

            // If there's no "src" attribute in output.Attributes this will noop.
            ProcessUrlAttribute(SrcAttributeName, output);

            // Retrieve the TagHelperOutput variation of the "src" attribute in case other TagHelpers in the
            // pipeline have touched the value. If the value is already encoded this ScriptTagHelper may
            // not function properly.
            Src = output.Attributes[SrcAttributeName]?.Value as string;

            // NOTE: Values in TagHelperOutput.Attributes may already be HTML-encoded.
            var attributes = new TagHelperAttributeList(output.Attributes);

            var builder = new DefaultTagHelperContent();

            if (!string.IsNullOrEmpty(SrcInclude))
            {
                BuildGlobbedScriptTags(attributes, builder);
                if (string.IsNullOrEmpty(Src))
                {
                    // Only SrcInclude is specified. Don't render the original tag.
                    output.TagName = null;
                    output.Content.SetContent(string.Empty);
                }
            }

            output.PostElement.SetContent(builder);
        }

        private void BuildGlobbedScriptTags(
            TagHelperAttributeList attributes,
            TagHelperContent builder)
        {
            EnsureGlobbingUrlBuilder();

            // Build a <script> tag for each matched src as well as the original one in the source file
            var staticUrl = HostingEnvironment.IsDevelopment() ? String.Format("http://localhost:{0}{1}", devSettings.WebpackDevServerPort, SrcInclude) : "";
            var includePattern = HostingEnvironment.IsDevelopment() ? "" : SrcInclude;

            var urls = GlobbingUrlBuilder.BuildUrlList(staticUrl, includePattern, "");
            foreach (var url in urls)
            {
                // "url" values come from bound attributes and globbing. Must always be non-null.
                Debug.Assert(url != null);

                if (string.Equals(url, Src, StringComparison.OrdinalIgnoreCase))
                {
                    // Don't build duplicate script tag for the original source url.
                    continue;
                }

                attributes[SrcAttributeName] = url;
                BuildScriptTag(attributes, builder);
            }
        }

        private void EnsureGlobbingUrlBuilder()
        {
            if (GlobbingUrlBuilder == null)
            {
                GlobbingUrlBuilder = new GlobbingUrlBuilder(
                    HostingEnvironment.WebRootFileProvider,
                    Cache,
                    ViewContext.HttpContext.Request.PathBase);
            }
        }

        private void BuildScriptTag(
            TagHelperAttributeList attributes,
            TagHelperContent builder)
        {
            builder.AppendHtml("<script");

            foreach (var attribute in attributes)
            {
                AppendAttribute(builder, attribute.Name, attribute.Value, escapeQuotes: false);
            }

            builder.AppendHtml("></script>");
        }

        private void AppendAttribute(TagHelperContent content, string key, object value, bool escapeQuotes)
        {
            content
                .AppendHtml(" ")
                .AppendHtml(key);
            if (escapeQuotes)
            {
                // Passed only JavaScript-encoded strings in this case. Do not perform HTML-encoding as well.
                content
                    .AppendHtml("=\\\"")
                    .AppendHtml((string)value)
                    .AppendHtml("\\\"");
            }
            else
            {
                // HTML-encoded the given value if necessary.
                content
                    .AppendHtml("=\"")
                    .Append(HtmlEncoder, ViewContext.Writer.Encoding, value)
                    .AppendHtml("\"");
            }
        }

    }
}