using BankAccountKata.Library.Interfaces;
using BankAccountKata.Library.Server;
using BankAccountKataGrpc.Host.BankAccountDb;
using BankAccountKataGrpc.Host.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<AccountRepository>();
builder.Services.AddScoped<IBankAccountOperationHandler, AccountRepository>();
builder.Services.AddScoped<IRequestUserAccountHandler, RequestUserAccountHandler>();
//builder.Services.AddHostedService<BankAccountKataServer>();
builder.Services.AddDbContextFactory<BankAccountDbContext>(
                    option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BankAccountKataServer>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

/* services.AddHostedService<Worker>();
                services.AddScoped<IBankAccountOperationHandler, AccountRepository>();
                services.AddScoped<IRequestUserAccountHandler, RequestUserAccountHandler>();
                services.AddSingleton<AccountRepository>();
                services.AddDbContextFactory<BankAccountDbContext>(
                    option => option.UseSqlServer("ConnectionString")); */