using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnbarForm.MainForm;
using AnbarService;
using Domain.Module;
using Infrastructure;

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