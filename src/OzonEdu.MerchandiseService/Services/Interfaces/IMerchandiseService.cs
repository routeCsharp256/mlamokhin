using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.Services.Interfaces
{
    public interface IMerchandiseService
    {
        Task<List<MerchandiseItem>> GetAll(CancellationToken token);

        Task<MerchandiseItem> GetById(long itemId, CancellationToken token);
    }
}