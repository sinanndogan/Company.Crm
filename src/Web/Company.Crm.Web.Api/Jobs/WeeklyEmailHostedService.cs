using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Usr;

namespace Company.Crm.Web.Api.Jobs;

public class WeeklyEmailHostedService : IHostedService, IDisposable
{
    private int _executionCount;
    private Timer? _timer;
    private readonly ILogger<TimedHostedService> _logger;
    private readonly IServiceProvider _services;

    public WeeklyEmailHostedService(ILogger<TimedHostedService> logger, IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("WeeklyEmail Hosted Service running.");

        using var scope = _services.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var allUsers = userService.GetAll();
        // db.JobsEmailSending.InsertAll()

        _timer = new Timer(SendEmail, allUsers, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private void SendEmail(object? state)
    {
        var allUsers = state as List<User>;

        var count = Interlocked.Increment(ref _executionCount);

        // Fire and Forget
        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday &&
            DateTime.Now is { Hour: 10, Minute: 9 } && count == 1)
        {
            foreach (var user in allUsers)
            {
                _logger.LogInformation($"Email sending. Count: {count}, Email: {user.Email}");
                // db.JobsEmailSending.Update(IsSent = true)
            }
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}