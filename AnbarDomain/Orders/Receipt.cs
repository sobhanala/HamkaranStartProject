using Domain.Common;
using System;

namespace AnbarDomain.Orders
{
    public class Receipt : BaseEntity
    {
        public string ReceiptNumber { get; set; }
        public ReciteType ReceiptType { get; set; }
        public int PartyId { get; set; }
        public decimal Discount { get; set; }
        public DateTime ReceiptTime { get; set; }
        public int WareHouseId { get; set; }
        public decimal TransportCost { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
