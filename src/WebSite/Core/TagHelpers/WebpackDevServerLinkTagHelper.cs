// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Globalization;
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
    /// <see cref="ITagHelper"/> implementation targeting &lt;link&gt; elements that supports webpack-dev-server.
    /// </summary>
    /// <remarks>
    /// The tag helper won't process for cases with just the 'href' attribute.
    /// </remarks>
    [HtmlTargetElement("link", Attributes = HrefIncludeAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class WebpackDevServerLinkTagHelper : UrlResolutionTagHelper
    {
        private const string HrefIncludeAttributeName = "webpack-dev-server-href-include";
        private const string HrefAttributeName = "href";

        private DevelopmentSettings devSettings;

        /// <summary>
        /// Creates a new <see cref="WebpackDevServerLinkTagHelper"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{ScriptTagHelper}"/>.</param>
        /// <param name="hostingEnvironment">The <see cref="IHostingEnvironment"/>.</param>
        /// <param name="cache">The <see cref="IMemoryCache"/>.</param>
        /// <param name="htmlEncoder">The <see cref="IHtmlEncoder"/>.</param>
        /// <param name="javaScriptEncoder">The <see cref="IJavaScriptStringEncoder"/>.</param>
        /// <param name="urlHelper">The <see cref="IUrlHelper"/>.</param>
        public WebpackDevServerLinkTagHelper(
            ILogger<WebpackDevServerLinkTagHelper> logger,
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
        /// Address of the linked resource.
        /// </summary>
        /// <remarks>
        /// Passed through to the generated HTML in all cases.
        /// </remarks>
        [HtmlAttributeName(HrefAttributeName)]
        public string Href { get; set; }

        /// <summary>
        /// A comma separated list of globbed file patterns of CSS stylesheets to load.
        /// The glob patterns are assessed relative to the application's 'webroot' setting.
        /// </summary>
        [HtmlAttributeName(HrefIncludeAttributeName)]
        public string HrefInclude { get; set; }

        protected ILogger<WebpackDevServerLinkTagHelper> Logger { get; }

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
            if (Href != null)
            {
                output.CopyHtmlAttribute(HrefAttributeName, context);
            }

            // If there's no "href" attribute in output.Attributes this will noop.
            ProcessUrlAttribute(HrefAttributeName, output);

            // Retrieve the TagHelperOutput variation of the "href" attribute in case other TagHelpers in the
            // pipeline have touched the value. If the value is already encoded this LinkTagHelper may
            // not function properly.
            Href = output.Attributes[HrefAttributeName]?.Value as string;

            // NOTE: Values in TagHelperOutput.Attributes may already be HTML-encoded.
            var attributes = new TagHelperAttributeList(output.Attributes);

            var builder = new DefaultTagHelperContent();

            if (!string.IsNullOrEmpty(HrefInclude))
            {
                BuildGlobbedLinkTags(attributes, builder);
                if (string.IsNullOrEmpty(Href))
                {
                    // Only HrefInclude is specified. Don't render the original tag.
                    output.TagName = null;
                    output.Content.SetContent(HtmlString.Empty);
                }
            }

            output.PostElement.SetContent(builder);
        }

        private void BuildGlobbedLinkTags(TagHelperAttributeList attributes, TagHelperContent builder)
        {
            EnsureGlobbingUrlBuilder();

            // Build a <link /> tag for each matched href.
            var staticUrl = HostingEnvironment.IsDevelopment() ? String.Format("http://localhost:{0}{1}", devSettings.WebpackDevServerPort, HrefInclude) : "";
            var includePattern = HostingEnvironment.IsDevelopment() ? "" : HrefInclude;

            var urls = GlobbingUrlBuilder.BuildUrlList(staticUrl, includePattern, "");
            foreach (var url in urls)
            {
                // "url" values come from bound attributes and globbing. Must always be non-null.
                Debug.Assert(url != null);

                if (string.Equals(Href, url, StringComparison.OrdinalIgnoreCase))
                {
                    // Don't build duplicate link tag for the original href url.
                    continue;
                }

                attributes[HrefAttributeName] = url;
                BuildLinkTag(attributes, builder);
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

        private void BuildLinkTag(TagHelperAttributeList attributes, TagHelperContent builder)
        {
            builder.AppendHtml("<link ");

            foreach (var attribute in attributes)
            {
                var attributeValue = attribute.Value;

                builder
                    .AppendHtml(attribute.Name)
                    .AppendHtml("=\"")
                    .Append(HtmlEncoder, ViewContext.Writer.Encoding, attribute.Value)
                    .AppendHtml("\" ");
            }

            builder.AppendHtml("/>");
        }
    }
}