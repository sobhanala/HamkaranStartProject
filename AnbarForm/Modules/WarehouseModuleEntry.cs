using System.Windows.Forms;
using Domain.Module;
using Infrastructure;

namespace AnbarForm.Modules
{
    public class WarehouseModuleEntry : IModule
    {

        public int Id => PasswordHasher.GetDeterministicHashCode(Subname);
        public string Name => "Party Management";
        public string Subname => "Manages warehouse operations like inventory, stock, and orders.";

        public void Initialize()
        {
            
        }

        public Form GetMainForm() => new MainForm.UserForm();
    }
}