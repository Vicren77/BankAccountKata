using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BankAccountKata.Host;
using BankAccountKata.Library;
using BankAccountKata.Host.BankAccountDb;
using BankAccountKata.Library.Interfaces;
using BankAccountKata.Host.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            //.UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
                services.AddScoped<IBankAccountOperationHandler, AccountRepository>();
                services.AddScoped<IRequestUserAccountHandler, RequestUserAccountHandler>();
                services.AddSingleton<AccountRepository>();
                services.AddDbContextFactory<BankAccountDbContext>(
                    option => option.UseSqlServer("ConnectionString"));
            });
}
