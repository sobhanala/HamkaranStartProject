using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace AnbarDomain.Orders
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public OrderType OrderType { get; set; }
        public int PartyId { get; set; }
        public DateTime OrderTime { get; set; }
        public Status Status { get; set; }
        public int WareHouseId { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
