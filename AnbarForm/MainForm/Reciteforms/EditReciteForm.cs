using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using AnbarService;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnbarForm.MainForm.Reciteforms
{   //TOOD this form  should be in this way that the master in top initialized and detail then
    public partial class EditReciteForm : Form
    {
        private AnbarDataSet _anbar;

        private AnbarDataSet.WarehouseReceiptsRow _receipt;
        private BindingSource _itemsBindingSource = new BindingSource();
        private readonly IWarehouseReceipt _warehouseReceiptService;
        private ProductsTableAdapter _productAdapter = new ProductsTableAdapter();

        public EditReciteForm(AnbarDataSet.WarehouseReceiptsRow receipt,
            IWarehouseReceipt warehouseReceipt)
        {
            _warehouseReceiptService = warehouseReceipt;
            _receipt = receipt;
            InitializeComponent();
            LoadReceiptDetails();
            LoadReceiptItems();
            LoadProducts();
        }

        // SOLUTION 1: Work with actual data table instead of view
        private void LoadReceiptItems()
        {
            LoadReceiptItemsFromView();

        }

        // SOLUTION 1A: Load actual receipt items and handle product info separately

        // SOLUTION 1B: If you must use view, handle changes manually
        private async void LoadReceiptItemsFromView()
        {
            _anbar = new AnbarDataSet();
            var viewTable = await _warehouseReceiptService.FillByReceiptIdWithProductInfo(receiptId: _receipt.Id);

            _itemsBindingSource.DataSource = viewTable;
            dataGridViewItems.DataSource = _itemsBindingSource;

            dataGridViewItems.CellValueChanged += DataGridViewItems_CellValueChanged;
            dataGridViewItems.CellEndEdit += DataGridViewItems_CellEndEdit;

            SetupGrid();
        }

        private void DataGridViewItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var grid = (DataGridView)sender;
                var row = grid.Rows[e.RowIndex];

                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
        }

        private void DataGridViewItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    var grid = (DataGridView)sender;
                    var rowView = grid.Rows[e.RowIndex].DataBoundItem as DataRowView;

                    if (rowView?.Row is AnbarDataSet.WarehouseReceiptItemsWithProductViewRow viewRow)
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating item: " + ex.Message);
                }
            }
        }

        private void LoadReceiptDetails()
        {
            lblReceiptNumber.Text = $"Receipt #{_receipt.ReceiptNumber}";
            lblReceiptDate.Text =
                $"Date: {(_receipt.IsReceiptDateNull() ? "N/A" : _receipt.ReceiptDate.ToShortDateString())}";
            lblReceiptType.Text = $"Type: {((ReciteType)_receipt.ReceiptStatus).ToString()}";
        }

        private void LoadProducts()
        {
            _productAdapter.Fill(_anbar.Products);
            cb_Products.DisplayMember = nameof(AnbarDataSet.ProductsRow.Name);
            cb_Products.ValueMember = nameof(AnbarDataSet.ProductsRow.Id);
            cb_Products.DataSource = new BindingSource { DataSource = _anbar.Products };
        }

        // Setup grid for editable data (not view)
        private void SetupGridForEditableData()
        {
            dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewItems.AllowUserToAddRows = false;
            dataGridViewItems.AllowUserToDeleteRows = true;

            // Add product name column as computed column or lookup
            if (!dataGridViewItems.Columns.Contains("ProductName"))
            {
                var productNameColumn = new DataGridViewComboBoxColumn
                {
                    Name = "ProductName",
                    HeaderText = "Product",
                    DataPropertyName = "ProductId",
                    DataSource = _anbar.Products,
                    DisplayMember = "Name",
                    ValueMember = "Id",
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                };
                dataGridViewItems.Columns.Add(productNameColumn);
            }

            // Make ID column read-only
            if (dataGridViewItems.Columns["Id"] != null)
            {
                dataGridViewItems.Columns["Id"].ReadOnly = true;
                dataGridViewItems.Columns["Id"].DefaultCellStyle.BackColor = Color.LightGray;
            }

            // Make ReceiptId read-only
            if (dataGridViewItems.Columns["ReceiptId"] != null)
            {
                dataGridViewItems.Columns["ReceiptId"].ReadOnly = true;
                dataGridViewItems.Columns["ReceiptId"].Visible = false;
            }
        }

        // Original setup for view
        private void SetupGrid()
        {
            dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var table = ((AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable)
                ((BindingSource)dataGridViewItems.DataSource).DataSource);

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

            // Make specific columns editable even in view
            if (dataGridViewItems.Columns["Quantity"] != null)
                dataGridViewItems.Columns["Quantity"].ReadOnly = false;

            if (dataGridViewItems.Columns["UnitPrice"] != null)
                dataGridViewItems.Columns["UnitPrice"].ReadOnly = false;
        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }

        private async void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                // End any pending edits
                dataGridViewItems.EndEdit();
                _itemsBindingSource.EndEdit();

                await _warehouseReceiptService.SaveReceiptItemsAndUpdateEwiAsync(_anbar, _receipt.Id);
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

                // SOLUTION 2: Add to actual table instead of view
                var newItem = _anbar.WarehouseReceiptItems.NewWarehouseReceiptItemsRow();
                newItem.ReceiptId = _receipt.Id;
                newItem.ProductId = productId;
                newItem.Quantity = quantity;
                newItem.UnitPrice = unitPrice;

                _anbar.WarehouseReceiptItems.AddWarehouseReceiptItemsRow(newItem);

                // Save immediately or mark as pending
                await _warehouseReceiptService.SaveReceiptItemsAndUpdateEwiAsync(_anbar, _receipt.Id);

                // Refresh the display
                LoadReceiptItems();

                // Clear input controls
                numeric_Quantity.Value = 0;
                numeric_UnitPrice.Value = 0;
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

                if (dataGridViewItems.CurrentRow?.DataBoundItem is DataRowView rowView)
                {
                    // SOLUTION 3: Handle both view and table scenarios
                    if (rowView.Row is AnbarDataSet.WarehouseReceiptItemsWithProductViewRow viewRow)
                    {
                        await _warehouseReceiptService.SaveChanges2TableAsync(_anbar.WarehouseReceiptItemsWithProductView);
                    }
                    else if (rowView.Row is AnbarDataSet.WarehouseReceiptItemsRow itemRow)
                    {
                        await UpdateItemFromTable(itemRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message);
            }
        }



        private async Task UpdateItemFromTable(AnbarDataSet.WarehouseReceiptItemsRow itemRow)
        {
            // Changes are already in the DataSet, just save
            await _warehouseReceiptService.SaveReceiptItemsAndUpdateEwiAsync(_anbar, _receipt.Id);
            MessageBox.Show("Item updated.");
        }

        // SOLUTION 4: Add delete functionality
        private async void BtnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewItems.CurrentRow?.DataBoundItem is DataRowView rowView)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this item?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (rowView.Row is AnbarDataSet.WarehouseReceiptItemsRow itemRow)
                        {
                            itemRow.Delete();
                            await _warehouseReceiptService.SaveReceiptItemsAndUpdateEwiAsync(_anbar, _receipt.Id);
                            MessageBox.Show("Item deleted.");
                        }
                        else if (rowView.Row is AnbarDataSet.WarehouseReceiptItemsWithProductViewRow viewRow)
                        {
                            // Find and delete from actual table
                            var actualItem = _anbar.WarehouseReceiptItems.FindById(viewRow.Id);
                            if (actualItem != null)
                            {
                                actualItem.Delete();
                                await _warehouseReceiptService.SaveReceiptItemsAndUpdateEwiAsync(_anbar, _receipt.Id);
                                LoadReceiptItems();
                                MessageBox.Show("Item deleted.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message);
            }
        }
    }
}