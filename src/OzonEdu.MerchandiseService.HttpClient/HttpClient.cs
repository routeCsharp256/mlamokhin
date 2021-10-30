using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace OzonEdu.MerchandiseService.HttpClient
{
    public interface IMerchandiseHttpClient
    {
        Task<List<MerchItemResponse>> GetAll(CancellationToken token);
        Task<MerchItemResponse> GetById(long id, CancellationToken token);
    }

    public class MerchandiseHttpClient : IMerchandiseHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public MerchandiseHttpClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MerchItemResponse>> GetAll(CancellationToken token)
        {
            using var response = await _httpClient.GetAsync("/api/merchandise", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<MerchItemResponse>>(body);
        }

        public async Task<MerchItemResponse> GetById(long id,CancellationToken token)
        {
            using var response = await _httpClient.GetAsync("/api/merchandise/id", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<MerchItemResponse>(body);
        }
        
    }
}