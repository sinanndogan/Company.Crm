using Microsoft.AspNetCore.Mvc.Filters;

namespace Company.Crm.Web.Mvc.Filters
{
    public class SampleActionFilter : ActionFilterAttribute
    {
        private readonly string _name;

        public SampleActionFilter(string name)
        {
            _name = name;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            Console.WriteLine($"Metot oncesi calisti. İsim={_name}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            Console.WriteLine($"Metot sonrasi calisti. İsim={_name}");
        }
    }

    public class ResponseHeaderAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public ResponseHeaderAttribute(string name, string value) =>
            (_name, _value) = (name, value);

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, _value);

            base.OnResultExecuting(context);
        }
    }
}
