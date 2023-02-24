using Company.Framework.Utilities;

namespace Company.Crm.Web.Mvc.Middlewares
{
    public class IpLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IpLoggingMiddleware> _logger;

        public IpLoggingMiddleware(RequestDelegate next, ILogger<IpLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logDirectory = Directory.GetCurrentDirectory() + "\\Logs";
            if (!Directory.Exists(logDirectory)) Directory.CreateDirectory(logDirectory);

            using var sw = File.AppendText(Path.Combine(logDirectory, "Logs.txt"));
            var ip = context.Connection.RemoteIpAddress.ToString();
            ip = NetUtility.GetPublicIP();
            var logMessage = ip + ";" + DateTime.Now;
            sw.WriteLine(logMessage);
            _logger.LogInformation(logMessage);

            await _next(context);
        }
    }

    public static class IPLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseIpLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpLoggingMiddleware>();
        }
    }
}