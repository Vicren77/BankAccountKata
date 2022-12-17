// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using BankAccountKataGrpc;
using Grpc.Core;

class Program
{
    private static async Task Main(string[] args)
    {
       
        var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { MaxReceiveMessageSize = 1 * 1024 * 1024 * 4 });
        var client = new BankAccountKataGrpc.BankAccountKata.BankAccountKataClient(channel);

        var request = await client.RequestCreateAccountAsync(new AccountEntity { Name = "Trump", Amount = 150000 });
        var depositrequest = await client.MakeDepositRequestAsync(new AccountEntity { Name = "Trump", Amount = 1 });
        Console.WriteLine(request.Amount + request.Name);
        Console.ReadKey();
    }
}