using System;
using AnbarDomain.Partys;
using Domain.Common;

namespace AnbarDomain.Inventory
{

    //TODO fix it  and have inventory and stock movement data table
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