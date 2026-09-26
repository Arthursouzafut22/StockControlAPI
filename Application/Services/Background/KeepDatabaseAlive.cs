using ControleMercadoria.Infrastructure.Repository.Movements;

namespace ControleMercadoria.Application.Services.Background
{
    public class KeepDatabaseAlive : BackgroundService
    {
        private readonly ILogger<KeepDatabaseAlive> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public KeepDatabaseAlive(IServiceScopeFactory scopeFactory, ILogger<KeepDatabaseAlive> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Rodando via cron!");

                using (var scope = _scopeFactory.CreateScope())
                {
                    var movementsRepository = scope.ServiceProvider
                        .GetRequiredService<IMovementsRepository>();
                    
                    await movementsRepository.GetAllMovementsWithProduct();
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
