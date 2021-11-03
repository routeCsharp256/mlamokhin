using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using OzonEdu.MerchandiseService.Grpc;

namespace OzonEdu.MerchandiseService.Grpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new MerchandiseApiGrpc.MerchandiseApiGrpcClient(channel);

            var items = await client.GetAllMerchandiseItemsAsync(new GetAllItemsRequest());

            try
            {
                var item = await client.GetMerchandiseItemByIdAsync(new GetItemByIdRequest() { ItemId = 1 });
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}