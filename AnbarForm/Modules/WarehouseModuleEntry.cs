using System.Collections.Generic;
using System.Windows.Forms;
using Domain.Module;
using AnbarForm.MainForm;
using Infrastructure;

namespace AnbarForm
{
    public class WarehouseModuleEntry : IModule
    {

        public int Id => PasswordHasher.GetDeterministicHashCode(Name);
        public string Name => "Warehouse Management";
        public string Description => "Manages warehouse operations like inventory, stock, and orders.";

        public void Initialize()
        {
            
        }

        public Form GetMainForm() => new MainForm.UserForm();
    }
}