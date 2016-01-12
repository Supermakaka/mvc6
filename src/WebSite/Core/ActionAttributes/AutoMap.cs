using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

using AutoMapper;

namespace WebSite
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutoMapAttribute : ActionFilterAttribute
    {
        public Type SourceType { get; private set; }

        public Type DestType{ get; private set; }

        public AutoMapAttribute(Type sourceType, Type destType)
        {
            SourceType = sourceType;
            DestType = destType;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = ((Controller)filterContext.Controller).ViewData.Model;

            object viewModel = Mapper.Map(model, SourceType, DestType);

            ((Controller)filterContext.Controller).ViewData.Model = viewModel;
        }
    }
}