using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.HttpClient.ConsoleClient
{
    class Program
    {
        private static IMerchandiseHttpClient _client = new MerchandiseHttpClient(new System.Net.Http.HttpClient());
        
        static async Task Main(string[] args)
        {

            var items=await _client.GetAll(CancellationToken.None);

            var item = await _client.GetById(1, CancellationToken.None);

        }
    }
}