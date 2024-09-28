using MassTransit;
using MessageContracts;

namespace ExampleWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                //Publishing the message every 10 seconds
                await _bus.Publish(new DeleteExamPaper
                { ExamName = "Test" });
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
