using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class AddServerToHeaderAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var settings = context.HttpContext.RequestServices.GetService(typeof(Settings)) as Settings;
            context.HttpContext.Response.Headers.Add("ServerIP", $"{settings.CurrentIP}:{settings.Port}");
            base.OnResultExecuting(context);
        }
    }
}
