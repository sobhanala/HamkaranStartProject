using Domain.Common;
using System;

namespace AnbarDomain.Inventory
{

    public class Inventory : BaseEntity
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
        public string BatchNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}