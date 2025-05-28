﻿using System.Windows.Forms;
using AnbarForm.MainForm.Reciteforms;
using AnbarService;
using Domain.Module;
using Domain.SharedSevices;
using Infrastructure;

namespace AnbarForm.Modules
{
    public class WarehouseModuleEntry : IModule
    {
        private readonly IWarehouseReceipt _warehouseReceiptService;
        private readonly IPartyManagement _partyManagement;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public WarehouseModuleEntry(IWarehouseReceipt warehouseReceiptService, IPartyManagement partyManagement, IUserService userService, IProductService productService)
        {
            _warehouseReceiptService = warehouseReceiptService;
            _partyManagement = partyManagement;
            _userService = userService;
            _productService = productService;
        }


        public int Id => PasswordHasher.GetDeterministicHashCode(Subname);
        public string Name => "Party Management";
        public string Subname => "Manages warehouse operations like inventory, stock, and orders.";

        public void Initialize()
        {
            
        }

        public Form GetMainForm() => new WarehouseReceiptForm(_warehouseReceiptService,_partyManagement,_userService,_productService);
    }
}