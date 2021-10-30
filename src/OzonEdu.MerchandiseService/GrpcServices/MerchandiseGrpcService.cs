using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OzonEdu.MerchandiseService.GrpcServices;
using OzonEdu.MerchandiseService.Infrastructure.Middlewares;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService:MerchandiseApiGrpc.MerchandiseApiGrpcBase
    {
        private readonly ILogger<MerchandiseGrpcService> _logger;
        private readonly IMerchandiseService _service;

        public MerchandiseGrpcService(IMerchandiseService service, ILogger<MerchandiseGrpcService> logger)
        {
            _service = service;
            _logger = logger;
        }

        public override async Task<ItemResponseUnit> GetMerchandiseItemById(GetItemByIdRequest request, ServerCallContext context)
        {
            var item = await _service.GetById(request.ItemId, context.CancellationToken);

            if (item == null) throw new RpcException(new Status(StatusCode.NotFound, "item not found"));
            
            return new ItemResponseUnit
            {
                Quantity = item.Quantity,
                ItemId = item.ItemId,
                ItemName = item.ItemName
            };
        }

        public override async Task<GetAllItemsResponse> GetAllMerchandiseItems(GetAllItemsRequest request, ServerCallContext context)
        {
            var items = await _service.GetAll( context.CancellationToken);

            return new GetAllItemsResponse
            {
                Stocks = { items.Select(x=>new ItemResponseUnit
                    ()
                    {
                        Quantity = x.Quantity,
                        ItemId = x.ItemId,
                        ItemName = x.ItemName
                    }) }
            };
        }
    }
}