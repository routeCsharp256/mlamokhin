using System;

namespace OzonEdu.MerchandiseService.HttpClient
{
    public class MerchItemResponse
    {
        public long ItemId { get; set; }
        
        public string ItemName { get; set; }
        
        public int Quantity { get; set; }
    }
}