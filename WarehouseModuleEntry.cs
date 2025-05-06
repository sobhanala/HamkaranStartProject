using System.Collections.Generic;
using System.Windows.Forms;
using Domain.Module;

namespace AnbarModule
{
    public class WarehouseModuleEntry : IModule
    {
        private readonly List<ISubModule> _subModules = new List<ISubModule>();

        public string Id => "Warehouse";
        public string Name => "Warehouse Management";
        public string Description => "Manages warehouse operations like inventory, stock, and orders.";

        public void Initialize()
        {
            _subModules.Add(new AddProduct.AddProductModule());
            _subModules.Add(new StockTransfer.StockTransferModule());
            _subModules.Add(new InventoryCount.InventoryCountModule());
        }

        public Form GetMainForm() => new MainForm.WarehouseMainForm();

        public IEnumerable<ISubModule> GetSubModules() => _subModules;
    }
}