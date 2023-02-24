using Company.Crm.MongoDb;
using Company.Framework.Utilities;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Security.Claims;

namespace Company.Crm.Web.Api.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly AuditLogRepository _mongodb;
        private readonly Stopwatch _stopWatch;

        public LogActionFilter(AuditLogRepository mongodb)
        {
            _mongodb = mongodb;
            _stopWatch = new Stopwatch();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopWatch.Start();

            //Log("OnActionExecuting", filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Log("OnActionExecuted", filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Log("OnResultExecuting", filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _stopWatch.Stop();

            var elapsed = _stopWatch.Elapsed.TotalMilliseconds;

            Log("OnResultExecuted", filterContext, elapsed);
        }

        private void Log(string methodName, FilterContext context, double? elapsed = null)
        {
            var user = context.HttpContext.User;
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            //var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            //Debug.WriteLine(message, "Action Filter Log");

            _mongodb.CreateAsync(new AuditLog
            {
                CustomData = methodName,
                ClientIpAddress = NetUtility.GetPublicIP(),
                ServiceName = controllerName.ToString(),
                MethodName = actionName.ToString(),
                UserId = Convert.ToInt64(user.FindFirstValue(ClaimTypes.NameIdentifier)),
                UserName = user.FindFirstValue(ClaimTypes.Name),
                ExecutionTime = DateTime.Now,
                ExecutionDuration = elapsed,
            }).GetAwaiter().GetResult();
        }
    }
}
