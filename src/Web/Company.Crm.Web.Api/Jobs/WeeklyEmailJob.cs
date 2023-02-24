using Company.Crm.Application.Services.Abstracts;
using FluentScheduler;

namespace Company.Crm.Web.Api.Jobs
{
    public class WeeklyEmailJob : IJob
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IServiceProvider _services;

        public WeeklyEmailJob(ILogger<TimedHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        public void Execute()
        {
            _logger.LogInformation("WeeklyEmail Job running.");

            using var scope = _services.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var allUsers = userService.GetAll();

            int count = 1;
            foreach (var user in allUsers)
            {
                _logger.LogInformation($"Email sending. Count: {count++}, Email: {user.Email}");
                // db.JobsEmailSending.Update(IsSent = true)
            }
        }
    }
}
