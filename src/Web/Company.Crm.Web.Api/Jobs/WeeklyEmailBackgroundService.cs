using Company.Crm.Application.Services.Abstracts;

namespace Company.Crm.Web.Api.Jobs;

public class WeeklyEmailBackgroundService : BackgroundService
{
    private readonly ILogger<TimedHostedService> _logger;
    private int executionCount = 0;
    private Timer? _timer = null;
    private IServiceScopeFactory _services;

    public WeeklyEmailBackgroundService(ILogger<TimedHostedService> logger, IServiceScopeFactory services)
    {
        _logger = logger;
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("WeeklyEmail Hosted Service executing.");

            SendEmail(null);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    public override Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("WeeklyEmail Hosted Service running.");

        return base.StartAsync(stoppingToken);
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        return base.StopAsync(stoppingToken);
    }

    private void SendEmail(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);

        // Fire and Forget
        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday &&
            DateTime.Now is { Hour: 12, Minute: 0 } && count == 1)
        {
            _logger.LogInformation("Email sending. Count: {Count}", count);
        }
    }
}