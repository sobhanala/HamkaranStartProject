using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace AnbarDomain.Orders
{
    public class OrderItems : BaseEntity
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public string UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}