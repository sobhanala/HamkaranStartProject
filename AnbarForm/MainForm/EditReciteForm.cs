using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using AnbarService;

namespace AnbarForm.MainForm
{
    public partial class EditReciteForm : Form
    {
        private AnbarDataSet _anbar;
        private AnbarDataSet.WarehouseReceiptsRow _receipt;
        private BindingSource _itemsBindingSource = new BindingSource();
        private readonly IWarehouseReceipt _warehouseReceiptService;
        private ProductsTableAdapter _partyAdapter = new ProductsTableAdapter();



        public EditReciteForm(AnbarDataSet anbar, AnbarDataSet.WarehouseReceiptsRow receipt, IWarehouseReceipt warehouseReceipt)
        {
            _warehouseReceiptService = warehouseReceipt;
            _anbar = anbar;
            _receipt = receipt;
            InitializeComponent();
            LoadReceiptDetails();
            LoadReceiptItems();
            SetupGrid();
            LoadProducts();
        }
        private void LoadReceiptItems()
        {
            var items = _receipt.GetWarehouseReceiptItemsRows();

            DataTable table;

            if (items.Any())
            {
                table = items.CopyToDataTable();
            }
            else
            {

                table = items.FirstOrDefault()?.Table.Clone() ?? new DataTable();
            }

            _itemsBindingSource.DataSource = table;
            dataGridViewItems.DataSource = _itemsBindingSource;
        }


        private void LoadReceiptDetails()
        {
            lblReceiptNumber.Text = $"Receipt #{_receipt.ReceiptNumber}";
            lblReceiptDate.Text = $"Date: {(_receipt.IsReceiptDateNull() ? "N/A" : _receipt.ReceiptDate.ToShortDateString())}";
            lblReceiptType.Text = $"Type: {((ReciteType)_receipt.ReceiptStatus).ToString()}";

        }

        private void LoadProducts()
        {
            _partyAdapter.Fill(_anbar.Products);   
            cb_Products.DisplayMember = nameof(AnbarDataSet.PartiesRow.Name);
            cb_Products.ValueMember = nameof(AnbarDataSet.PartiesRow.Id);
            cb_Products.DataSource = new BindingSource { DataSource = _anbar.Products };
        }


        private void SetupGrid()
        {
            
            dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }




        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private async  void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
               await  _warehouseReceiptService.SaveReceiptItemsAndUpdateEwiAsync(_anbar,_receipt.Id);
                MessageBox.Show("Items saved successfully.");
                this.DialogResult = DialogResult.OK; 
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving items: " + ex.Message);
            }


        }

        private void BtnAddItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                var productId = (int)cb_Products.SelectedValue;
                var quantity = (int)numeric_Quantity.Value;
                var unitPrice = numeric_Quantity.Value;

                var newItem = _anbar.WarehouseReceiptItems.NewWarehouseReceiptItemsRow();
                newItem.ReceiptId = _receipt.Id;
                newItem.ProductId = productId;
                newItem.Quantity = quantity;
                newItem.UnitPrice = unitPrice;

                _anbar.WarehouseReceiptItems.AddWarehouseReceiptItemsRow(newItem);

                LoadReceiptItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message);
            }
        }

    }
}
