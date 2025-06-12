using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using AnbarForm.MainForm.Reciteforms.selectors;
using AnbarService;
using Domain.SharedSevices;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AnbarDomain.Tabels.warhousesTableAdapters;
using AnbarService.Interfaces;
using Domain.Exceptions;
using Infrastructure;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class ReciteAnbarFormincome : Form
    {
        private AnbarDataSet _currentDataSet;
        private readonly BindingSource _headerBindingSource = new BindingSource();

        private readonly BindingSource _detailBindingSource = new BindingSource();
        private readonly WarehousesTableAdapter _warehousesTableAdapter = new WarehousesTableAdapter();
        private readonly IWarehouseReceipt _warehouseReceiptService;
        private readonly IPartyManagement _partyManagement;
        private readonly IProductService _productService;
        private readonly int? _receiptId;



        public ReciteAnbarFormincome(IUserService userService, IWarehouseReceipt warehouseReceiptService,
            IPartyManagement partyManagement, IProductService productService, int? receiptId=null)
        {
            _warehouseReceiptService = warehouseReceiptService;
            _partyManagement = partyManagement;
            _productService = productService;
            _receiptId = receiptId;
            InitializeComponent();
            ConfigureComboBox();
        }



        private void MapToBindigSource()
        {

            warehouseId.DataBindings.Add("Text", _headerBindingSource, "WarehouseId", true, DataSourceUpdateMode.OnPropertyChanged);
            partyId.DataBindings.Add("Text", _headerBindingSource, "PartyId", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxWarhouse.DataBindings.Add("Text", _headerBindingSource, "WarehouseName", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxParty.DataBindings.Add("Text", _headerBindingSource, "PartyName", true, DataSourceUpdateMode.OnPropertyChanged);
            dateTimePicker1.DataBindings.Add("Value", _headerBindingSource, "ReceiptDate", true, DataSourceUpdateMode.OnPropertyChanged);
            numCost.DataBindings.Add("Value", _headerBindingSource, "TransportCost", true, DataSourceUpdateMode.OnPropertyChanged);
            numDiscount.DataBindings.Add("Value", _headerBindingSource, "Discount", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbType.DataBindings.Add("SelectedValue", _headerBindingSource, "ReceiptStatus", true, DataSourceUpdateMode.OnPropertyChanged);


        }

        private void ConfigureComboBox()
        {
            var receiptTypes = Enum.GetValues(typeof(ReciteType)).Cast<ReciteType>().Select
               (u => new { Name = u.ToString(), Value = (byte)u }).ToList();

            cmbType.DisplayMember = "Name";
            cmbType.ValueMember = "Value";
            cmbType.DataSource = receiptTypes;
            if (_receiptId.HasValue)
            {
                cmbType.Enabled = false;
            }

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private async void Btn_Save_Click(object sender, EventArgs e)
        {
            if (!_receiptId.HasValue)
            {
                var receiptNumber = await UiSafeExecutor.ExecuteAsync(() => _warehouseReceiptService.GenerateNewReceiptNumber());
                PrepareDataSetBeforeSave(receiptNumber);
            }

            _headerBindingSource.EndEdit();
            _detailBindingSource.EndEdit();

           var result =  await UiSafeExecutor.ExecuteAsync(() => _warehouseReceiptService.SaveMasterDetailAsync(_currentDataSet));

           if (!result)
           {
               return;
           }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PrepareDataSetBeforeSave(string resitenum)
        {
            _headerBindingSource.EndEdit();

            if (_headerBindingSource.Current is DataRowView rowView &&
                rowView.Row is AnbarDataSet.view_WarehouseReceiptsRow receiptRow)
            {
                receiptRow.ReceiptNumber = resitenum;
            }
        }

        private async void ReciteAnbarForm_Load(object sender, EventArgs e)
        {
            _currentDataSet = new AnbarDataSet();
            if (_receiptId.HasValue)
            {
                 await UiSafeExecutor.ExecuteAsync(() => _warehouseReceiptService.FillReceiptById(dataSet:_currentDataSet, _receiptId.Value));
            }
            else
            {
                AddTempMasterRow();
            }
            _headerBindingSource.DataSource = _currentDataSet;
            _headerBindingSource.DataMember = _currentDataSet.view_WarehouseReceipts.TableName;

            _detailBindingSource.DataSource = _currentDataSet.WarehouseReceiptItemsWithProductView; ;

            dgReciteItem.AutoGenerateColumns = true;
            dgReciteItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgReciteItem.DataSource = _detailBindingSource;

            MapToBindigSource();
            ConfigureBindingSource();
            ConfigDataGrid();
            BindTotalAmountInitial();
        }

        private void ConfigureBindingSource()
        {
            _headerBindingSource.ListChanged += HeaderBindingSource_ListChanged;
            _detailBindingSource.CurrentItemChanged += _detailBindingSource_CurrentItemChanged1;
            numCost.ValueChanged += (s, e) => _headerBindingSource.EndEdit();
            numDiscount.ValueChanged += (s, e) => _headerBindingSource.EndEdit();

        }

        private void _detailBindingSource_CurrentItemChanged1(object sender, EventArgs e)
        {
            RecalculateCurrentRowTotalAndUpdate();
            UpdateTotalAmount();

        }


        private void HeaderBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                UpdateTotalAmount();
            }
        }

        private void RecalculateCurrentRowTotalAndUpdate()
        {
            if (_detailBindingSource.Current is DataRowView rowView)
            {
                if (rowView.Row is AnbarDataSet.WarehouseReceiptItemsWithProductViewRow row)
                {
                    decimal quantity = row.IsQuantityNull() ? 0 : row.Quantity;
                    decimal unitPrice = row.IsUnitPriceNull() ? 0 : row.UnitPrice;

                    row.TotalAmount = quantity * unitPrice;
                }
            }

            UpdateTotalAmount();
        }





        private void AddTempMasterRow()
        {
            var newMasterRow = _currentDataSet.view_WarehouseReceipts.Newview_WarehouseReceiptsRow();
            newMasterRow.Id = -1;
            newMasterRow.ReceiptDate = DateTime.Now;
            newMasterRow.TransportCost = 0m;
            newMasterRow.Discount = 0m;
            newMasterRow.TotalAmount = 0;
            newMasterRow.PartyId = 0;
            newMasterRow.WarehouseId = 0;
            newMasterRow.WarehouseName = "";
            newMasterRow.PartyName = "";
            newMasterRow.PartyType = 0;
            newMasterRow.ReceiptStatus = 0;
            _currentDataSet.view_WarehouseReceipts.Addview_WarehouseReceiptsRow(newMasterRow);
        }

        #region Selectors

        private void BtnSelectWarehouse_Click_1(object sender, EventArgs e)
        {
            var anbar = new warhouses();
            _warehousesTableAdapter.Fill(anbar.Warehouses);

            var selector = new SelectorForm<warhouses.WarehousesDataTable>(anbar.Warehouses, "name", "Warehouse");
            selector.StartPosition = FormStartPosition.Manual;

            var btn = sender as Button;
            var buttonPosition = btn.PointToScreen(Point.Empty);

            selector.Location = new Point(buttonPosition.X, buttonPosition.Y + btn.Height);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var selectedRow = (warhouses.WarehousesRow)selector.SelectedRow;

                if (selectedRow != null)
                {

                    textBoxWarhouse.Text = selectedRow.Name;
                    warehouseId.Text = selectedRow.Id.ToString();
                }
            }
        }

        private async void BtnSelectParty_Click_1(object sender, EventArgs e)
        {
            var anbar = await UiSafeExecutor.ExecuteAsync(() => _partyManagement.GetPartyDataSetAsync());
            var selector = new SelectorForm<warhouses.PartiesDataTable>(anbar.Parties, "name", "Party");
            selector.StartPosition = FormStartPosition.Manual;

            var btn = sender as Button;
            var buttonPosition = btn.PointToScreen(Point.Empty);

            selector.Location = new Point(buttonPosition.X, buttonPosition.Y + btn.Height);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var selectedRow = (warhouses.PartiesRow)selector.SelectedRow;
                if (selectedRow != null)
                {

                    textBoxParty.Text = selectedRow.Name;
                    partyId.Text = selectedRow.Id.ToString();
                }
            }
        }

        #endregion

        private async void DgReciteItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgReciteItem.Columns[e.ColumnIndex].Name == "ProductSelector")
            {
                var anbar = await UiSafeExecutor.ExecuteAsync(() => _productService.GetDataSet());

                var selector = new SelectorForm<ProductDataset.ProductsDataTable>(anbar.Products, "name", "Product");
                selector.StartPosition = FormStartPosition.Manual;

                var gridLocation = dgReciteItem.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var screenPoint = dgReciteItem.PointToScreen(gridLocation.Location);
                selector.Location = new Point(screenPoint.X, screenPoint.Y + gridLocation.Height);

                if (selector.ShowDialog() == DialogResult.OK)
                {
                    var selectedRow = (ProductDataset.ProductsRow)selector.SelectedRow;
                    if (selectedRow != null)
                    {
                        var row = dgReciteItem.Rows[e.RowIndex];

                        row.Cells["ProductName"].Value = selectedRow.Name;
                        row.Cells["ProductId"].Value = selectedRow.Id;
                        row.Cells["ProductCode"].Value = selectedRow.ProductCode;
                        row.Cells["ProductName"].ReadOnly = true;
                        row.Cells["ProductId"].ReadOnly = true;
                        row.Cells["ProductCode"].ReadOnly = true;

                        if (row.Cells["Quantity"].Value == null)
                            row.Cells["Quantity"].Value = 0;
                        if (row.Cells["UnitPrice"].Value == null)
                            row.Cells["UnitPrice"].Value = 0m;

                    }
                }
            }
        }


        private void ConfigDataGrid()
        {
            if (!dgReciteItem.Columns.Contains("ProductSelector"))
            {
                var partyButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "ProductSelector",
                    HeaderText = "Product",
                    Text = "Select Product",
                    UseColumnTextForButtonValue = true
                };
                dgReciteItem.Columns.Add(partyButtonColumn);
            }

            if (dgReciteItem.Columns.Contains("TotalAmount"))
            {
                dgReciteItem.Columns["TotalAmount"].ReadOnly = true;
                dgReciteItem.Columns["TotalAmount"].Visible = true;
            }
            if (dgReciteItem.Columns.Contains("Id"))
            {
                dgReciteItem.Columns["Id"].ReadOnly = true;
                dgReciteItem.Columns["Id"].Visible = false;

            }
        }






        #region Total handeling



        private decimal CalculateTotalAmount()
        {
            decimal subtotal = 0;

            foreach (DataRowView rowView in _detailBindingSource)
            {
                var row = (AnbarDataSet.WarehouseReceiptItemsWithProductViewRow)rowView.Row;

                if (!row.IsTotalAmountNull())
                {
                    subtotal += row.TotalAmount;
                }
            }

            _headerBindingSource.EndEdit();

            decimal discount = 0m;
            decimal cost = 0m;

            if (_headerBindingSource.Current is DataRowView headerView &&
                headerView.Row is AnbarDataSet.view_WarehouseReceiptsRow headerRow)
            {
                if (!headerRow.IsDiscountNull())
                    discount = headerRow.Discount;

                if (!headerRow.IsTransportCostNull())
                    cost = headerRow.TransportCost;
            }

            decimal total = subtotal * (1 - discount / 100m) + cost;
            return total;
        }


        private void UpdateTotalAmount()
        {
            var total = CalculateTotalAmount();

            TotalAmount.Text = $"Total Amount: ${total:F2}";

        }
        private void BindTotalAmountInitial()
        {
            if (_headerBindingSource.Current is DataRowView headerView &&
                headerView.Row is AnbarDataSet.view_WarehouseReceiptsRow row &&
                !row.IsTotalAmountNull())
            {
                TotalAmount.Text = $"Total Amount: ${row.TotalAmount:F2}";
            }
            else
            {
                TotalAmount.Text = "Total Amount: $0.00";
            }
        }



        #endregion



        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                _headerBindingSource.EndEdit();
                if (_headerBindingSource.Current is DataRowView masterRowView &&
                    masterRowView.Row is AnbarDataSet.view_WarehouseReceiptsRow masterRow)
                {

                    var newDetailRow = _currentDataSet.WarehouseReceiptItemsWithProductView.NewWarehouseReceiptItemsWithProductViewRow();
                    newDetailRow.ReceiptId = masterRow.Id;
                    newDetailRow.ProductId = 0;
                    newDetailRow.ProductName = "";
                    newDetailRow.ProductCode = "";
                    newDetailRow.Quantity = 0;
                    newDetailRow.UnitPrice = 0m;
                    newDetailRow.TotalAmount = 0m;
                    _currentDataSet.WarehouseReceiptItemsWithProductView.AddWarehouseReceiptItemsWithProductViewRow(newDetailRow);

                    _detailBindingSource.ResetBindings(false);
                    UpdateTotalAmount();
                    Debug.WriteLine("All dataset rows:");
                    foreach (DataRow row in _currentDataSet.WarehouseReceiptItemsWithProductView.Rows)
                    {
                        Debug.WriteLine($"Id: {row["Id"]}, ReceiptId: {row["ReceiptId"]}");
                    }

                    Debug.WriteLine("Filtered (visible) rows in binding source:");
                    foreach (DataRowView view in _detailBindingSource)
                    {
                        Debug.WriteLine($"Id: {view["Id"]}, ReceiptId: {view["ReceiptId"]}");
                    }

                }

                else
                {
                    MessageBox.Show("Please create or select a master receipt first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void BtnDeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgReciteItem.CurrentRow != null && !dgReciteItem.CurrentRow.IsNewRow)
                {
                    if (dgReciteItem.CurrentRow.DataBoundItem is DataRowView rowView)
                    {
                        rowView.Row.Delete();
                        _detailBindingSource.ResetBindings(false);
                        UpdateTotalAmount();
                    }
                    else
                    {
                        dgReciteItem.Rows.Remove(dgReciteItem.CurrentRow);
                        UpdateTotalAmount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}