using AnbarService;
using Domain.Module;
using Infrastructure;
using System.Windows.Forms;

namespace AnbarForm.Modules
{

    public class ProductModule : IModule
    {
        private readonly IProductService _productService;

        public ProductModule(IProductService productService)
        {
            _productService = productService;
        }
        public int Id => PasswordHasher.GetDeterministicHashCode(Subname);
        public string Name => "Product Management";
        public string Subname => "Add , Delete ,Update Tour Products";

        public void Initialize()
        {
        }


        public Form GetMainForm()
        {
            return new MainForm.ProductManagementForm(_productService);
        }
    }

}