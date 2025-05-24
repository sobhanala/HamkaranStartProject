using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            LoadProducts();
        }
        private async void LoadReceiptItems()
        {
            var viewTable = await _warehouseReceiptService.FillByReceiptIdWithProductInfo(receiptId:_receipt.Id);

            _itemsBindingSource.DataSource = viewTable.WarehouseReceiptItemsWithProductView;
            dataGridViewItems.DataSource = _itemsBindingSource;
            SetupGrid();



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

            var table = ((AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable)((BindingSource)dataGridViewItems.DataSource).DataSource);

            foreach (DataGridViewColumn gridColumn in dataGridViewItems.Columns)
            {
                if (table.Columns.Contains(gridColumn.DataPropertyName))
                {
                    var dataColumn = table.Columns[gridColumn.DataPropertyName];

                    gridColumn.ReadOnly = dataColumn.ReadOnly;

                    if (dataColumn.ReadOnly)
                    {
                        gridColumn.DefaultCellStyle.BackColor = Color.Chocolate;
                    }
                }
            }
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

        private async void BtnAddItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                var productId = (int)cb_Products.SelectedValue;
                var quantity = (int)numeric_Quantity.Value;
                var unitPrice = numeric_UnitPrice.Value;

                var newItem = _anbar.WarehouseReceiptItems.NewWarehouseReceiptItemsRow();
                newItem.ReceiptId = _receipt.Id;
                newItem.ProductId = productId;
                newItem.Quantity = quantity;
                newItem.UnitPrice = unitPrice;

                _anbar.WarehouseReceiptItems.AddWarehouseReceiptItemsRow(newItem);

               await  _warehouseReceiptService.SaveReceiptItemsAndUpdateEwiAsync(dataset: _anbar, receiptId: _receipt.Id);

                 LoadReceiptItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message);
            }
        }

        private async void Btn_Updateitem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewItems.EndEdit();
                _itemsBindingSource.EndEdit();

                foreach (AnbarDataSet.WarehouseReceiptItemsWithProductViewRow viewRow in _anbar.WarehouseReceiptItemsWithProductView)
                {
                    Debug.WriteLine($"Row ID: {viewRow.Id}, State: {viewRow.RowState}, Qty: {viewRow.Quantity}");
                }
                await _warehouseReceiptService.SaveChangesTableAsync(_anbar.WarehouseReceiptItemsWithProductView);

                MessageBox.Show("Updates saved successfully.");
                LoadReceiptItems(); // Refresh with updated values
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating items: " + ex.Message);
            }
        }

    }
}
