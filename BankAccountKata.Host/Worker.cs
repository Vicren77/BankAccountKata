using BankAccountKata.Host.BankAccountDb;
using BankAccountKata.Library.Server;
using Microsoft.EntityFrameworkCore;

namespace BankAccountKata.Host
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BankAccountKataServer _bankAccountServer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                //add my banking service here 

            }

        }
    }
}