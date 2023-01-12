// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using BankAccountKata;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using BankAccountKataGrpc.Host.BankAccountDb;

class Program
{
    private static async Task Main(string[] args)
    {

        var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { MaxReceiveMessageSize = 1 * 1024 * 1024 * 4 });
        var client = new BankAccountKata.BankAccountKata.BankAccountKataClient(channel);

        var request = await client.RequestCreateAccountAsync(new AccountEntity { Name = "", Amount = 150000 });
        var depositRequest = await client.MakeDepositRequestAsync(new AccountEntity { Name = "Trump", Amount = 54 });
        var withdrawRequest = await client.MakeWithdrawRequestAsync((new AccountEntity { Name = "Trump", Amount = 71 }));

        var getTransacResponse =  client.GetHistory( new HistoryRequest { Name = "Trump"});
        var responseStream = getTransacResponse.ResponseStream;
        while(await responseStream.MoveNext())
        {
            Console.WriteLine(responseStream.Current);
        }
        Console.ReadKey();
    }
}