using System.Windows.Forms;
using AnbarService;
using Domain.Module;
using Infrastructure;

namespace AnbarForm.Modules
{
    public class WarehouseModuleEntry : IModule
    {
        private readonly IWarehouseReceipt _warehouseReceiptService;

        public WarehouseModuleEntry(IWarehouseReceipt warehouseReceiptService)
        {
            _warehouseReceiptService = warehouseReceiptService;
        }


        public int Id => PasswordHasher.GetDeterministicHashCode(Subname);
        public string Name => "Party Management";
        public string Subname => "Manages warehouse operations like inventory, stock, and orders.";

        public void Initialize()
        {
            
        }

        public Form GetMainForm() => new MainForm.WarehouseReceiptForm(_warehouseReceiptService);
    }
}