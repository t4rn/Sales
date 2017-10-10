using System;
using System.Text;
using System.Web.Mvc;

namespace Sales.Web.ViewModels
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class HandleModelStateExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentException(nameof(filterContext));
            }

            if (filterContext.Exception != null
                && typeof(ModelStateException).IsInstanceOfType(filterContext.Exception)
                && !filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.Result = new ContentResult()
                {
                    Content = (filterContext.Exception as ModelStateException).Message,
                    ContentEncoding = Encoding.UTF8
                };
            }
        }
    }
}
