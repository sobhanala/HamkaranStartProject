using Domain.Common;

namespace AnbarDomain.Shipping
{
    public class StockMovement : BaseEntity
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
    }
}
