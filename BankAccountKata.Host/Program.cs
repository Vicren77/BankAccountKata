using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BankAccountKata.Host;
using BankAccountKata.Host.BankAccountDb;

IHost host = Host.CreateDefaultBuilder(args)
    .UseEnvironment("Development")
    .ConfigureServices((hostContext,services) =>
    {
        services.AddHostedService<Worker>();
        services.AddDbContext<BankAccountDbContext>(
            option => option.UseSqlServer("ConnectionString"));
    })
    .Build();

await host.RunAsync();
