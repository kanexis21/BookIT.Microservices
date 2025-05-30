namespace StatusUpdaterService.Core.Application.Services
{
    public class StatusUpdateHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<StatusUpdateHostedService> _logger;

        public StatusUpdateHostedService(IServiceProvider serviceProvider, ILogger<StatusUpdateHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IStatusUpdateService>();

                try
                {
                    await service.UpdateStatusesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при обновлении статусов");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
