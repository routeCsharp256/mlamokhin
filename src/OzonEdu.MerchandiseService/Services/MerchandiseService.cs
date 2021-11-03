using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Services
{
    public class MerchandiseService:IMerchandiseService
    {
        private readonly List<MerchandiseItem> MerchandiseItems = new List<MerchandiseItem>
        {
            new MerchandiseItem(1, "Футболка", 10),
            new MerchandiseItem(2, "Толстовка", 20),
            new MerchandiseItem(3, "Кепка", 15)
        };
        
        
        public Task<List<MerchandiseItem>> GetAll(CancellationToken token)
        {
            return Task.FromResult(MerchandiseItems);
        }

        public Task<MerchandiseItem> GetById(long itemId, CancellationToken token)
        {
            var item = MerchandiseItems.FirstOrDefault(e => e.ItemId == itemId);
            return Task.FromResult(item);
        }
    }
}