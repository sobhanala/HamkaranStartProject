using Domain.Common;

namespace AnbarDomain.Orders
{
    public class ReceiptItems : BaseEntity
    {
        public int ProductId { get; set; }
        public int ReceiptId { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}