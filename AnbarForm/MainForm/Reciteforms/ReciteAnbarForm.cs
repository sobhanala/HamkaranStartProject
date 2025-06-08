using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using AnbarForm.MainForm.Reciteforms.selectors;
using AnbarService;
using Domain.SharedSevices;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Domain.Exceptions;
using Infrastructure;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class ReciteAnbarFormincome : Form


    {
        private AnbarDataSet _currentDataSet;



        private readonly BindingSource _headerBindingSource = new BindingSource();
        private AnbarDataSet.WarehousesRow ReciteHeaderWarehousesRow { get; set; }
        private AnbarDataSet.PartiesRow ReciteHeaderPartiesRow { get; set; }


        private readonly BindingSource _detailBindingSource = new BindingSource();
        private readonly WarehousesTableAdapter _warehousesTableAdapter = new WarehousesTableAdapter();

        private readonly IWarehouseReceipt _warehouseReceiptService;
        private readonly IPartyManagement _partyManagement;
        private readonly IProductService _productService;


        public ReciteAnbarFormincome(IUserService userService, IWarehouseReceipt warehouseReceiptService,
            IPartyManagement partyManagement, IProductService productService)
        {
            _warehouseReceiptService = warehouseReceiptService;
            _partyManagement = partyManagement;
            _productService = productService;
            InitializeComponent();
            ConfigureComboBox();
        }

        private void MapToBindigSource()
        {
            textBoxWarhouse.DataBindings.Add("Text", _headerBindingSource, "WarehouseId", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxParty.DataBindings.Add("Text", _headerBindingSource, "PartyId", true, DataSourceUpdateMode.OnPropertyChanged);
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
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void Btn_Save_Click(object sender, EventArgs e)
        {

            var receiptNumber =await  UiSafeExecutor.ExecuteAsync( () =>  _warehouseReceiptService.GenerateNewReceiptNumber());

            PrepareDataSetBeforeSave(receiptNumber);

            await UiSafeExecutor.ExecuteAsync(()=>   _warehouseReceiptService.SaveReceiptWithItemsAsync(_currentDataSet));

            MessageBox.Show("Receipt saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PrepareDataSetBeforeSave(string resitenum)
        {
            if (_currentDataSet == null) return;

            _currentDataSet.WarehouseReceipts.Clear();
            var receiptRow = _currentDataSet.WarehouseReceipts.NewWarehouseReceiptsRow();

            receiptRow.ReceiptDate = dateTimePicker1.Value;

            if (ReciteHeaderPartiesRow != null)
                receiptRow.PartyId = ReciteHeaderPartiesRow.Id;

            if (ReciteHeaderWarehousesRow != null)
                receiptRow.WarehouseId = ReciteHeaderWarehousesRow.Id;
            receiptRow.ReceiptNumber = resitenum;

            if (numCost.Value is decimal transportCost)
            {
                receiptRow.TransportCost = transportCost;
            }
            if (numDiscount.Value is decimal discount)
            {
                receiptRow.Discount = discount;
            }
            if (cmbType.SelectedValue is byte type)
            {
                receiptRow.ReceiptStatus = type;
            }
            _currentDataSet.WarehouseReceipts.AddWarehouseReceiptsRow(receiptRow);
        }

        private void ReciteAnbarForm_Load(object sender, EventArgs e)
        {
            _currentDataSet = new AnbarDataSet();
            addTempMasterRow();

            _headerBindingSource.DataSource = _currentDataSet;
            _headerBindingSource.DataMember = _currentDataSet.WarehouseReceipts.TableName;

            _detailBindingSource.DataSource = _headerBindingSource;
            _detailBindingSource.DataMember = "WarehouseReceipts_WarehouseReceiptItemsWithProductView";

            dgReciteItem.AutoGenerateColumns = true;
            dgReciteItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgReciteItem.DataSource = _detailBindingSource;


            MapToBindigSource();
            dgReciteItem.DataSource = _detailBindingSource;
            AddPartyButtonColumn();


        }

        private void addTempMasterRow()
        {
            var newMasterRow = _currentDataSet.WarehouseReceipts.NewWarehouseReceiptsRow();
            newMasterRow.ReceiptDate = DateTime.Now;  // example default value
            newMasterRow.TransportCost = 0m;
            newMasterRow.Discount = 0m;
            newMasterRow.TotalAmount = 0;
            newMasterRow.PartyId = 0;
            newMasterRow.WarehouseId = 0;
            newMasterRow.ReceiptStatus = 0;
            _currentDataSet.WarehouseReceipts.AddWarehouseReceiptsRow(newMasterRow);
        }



        #region Selectors

        private void BtnSelectWarehouse_Click_1(object sender, EventArgs e)
        {

            var anbar = new AnbarDataSet();
            _warehousesTableAdapter.Fill(anbar.Warehouses);

            var selector = new SelectorForm<AnbarDataSet.WarehousesDataTable>(anbar.Warehouses, "name", "Warehouse");
            selector.StartPosition = FormStartPosition.Manual;

            var btn = sender as Button;
            var buttonPosition = btn.PointToScreen(Point.Empty);

            selector.Location = new Point(buttonPosition.X, buttonPosition.Y + btn.Height);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var selectedRow = (AnbarDataSet.WarehousesRow)selector.SelectedRow;

                if (selectedRow != null)
                {
                    ReciteHeaderWarehousesRow = selectedRow;
                    textBoxWarhouse.Text = selectedRow.Name;
                }
            }

        }

        private async void BtnSelectParty_Click_1(object sender, EventArgs e)
        {
            var anbar = await UiSafeExecutor.ExecuteAsync(()=>  _partyManagement.GetPartyDataSetAsync());
            var selector = new SelectorForm<AnbarDataSet.PartiesDataTable>(anbar.Parties, "name", "Party");
            selector.StartPosition = FormStartPosition.Manual;

            var btn = sender as Button;
            var buttonPosition = btn.PointToScreen(Point.Empty);

            selector.Location = new Point(buttonPosition.X, buttonPosition.Y + btn.Height);

            if (selector.ShowDialog() == DialogResult.OK)
            {
                var selectedRow = (AnbarDataSet.PartiesRow)selector.SelectedRow;
                if (selectedRow != null)
                {
                    ReciteHeaderPartiesRow = selectedRow;
                    textBoxParty.Text = selectedRow.Name;
                }
            }
        }



        #endregion

        private async void DgReciteItem_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dgReciteItem.Columns[e.ColumnIndex].Name == "ProductSelector")
            {
                var anbar = await UiSafeExecutor.ExecuteAsync(() =>   _productService.GetDataSet());

                var selector = new SelectorForm<AnbarDataSet.ProductsDataTable>(anbar.Products, "name", "Product");
                selector.StartPosition = FormStartPosition.Manual;

                var gridLocation = dgReciteItem.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var screenPoint = dgReciteItem.PointToScreen(gridLocation.Location);
                selector.Location = new Point(screenPoint.X, screenPoint.Y + gridLocation.Height);

                if (selector.ShowDialog() == DialogResult.OK)
                {
                    var selectedRow = (AnbarDataSet.ProductsRow)selector.SelectedRow;
                    if (selectedRow != null)
                    {
                        var row = dgReciteItem.Rows[e.RowIndex];

                        row.Cells["ProductName"].Value = selectedRow.Name;
                        row.Cells["ProductId"].Value = selectedRow.Id;
                        row.Cells["ProductCode"].Value = selectedRow.ProductCode;
                        row.Cells["ProductName"].ReadOnly = true;
                        row.Cells["ProductId"].ReadOnly = true;
                        row.Cells["ProductCode"].ReadOnly = true;
                    }
                }
            }
        }

        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            _headerBindingSource.EndEdit();

            if (_headerBindingSource.Current is DataRowView masterRowView &&
                masterRowView.Row is AnbarDataSet.WarehouseReceiptsRow masterRow)
            {
                var detailTable = _currentDataSet.WarehouseReceiptItemsWithProductView;

                var newDetailRow = detailTable.NewWarehouseReceiptItemsWithProductViewRow();

                newDetailRow.ProductId = 0;
                newDetailRow.ProductName = "";
                newDetailRow.ProductCode = "";
                newDetailRow.Quantity = 0;
                newDetailRow.UnitPrice = 0;
                newDetailRow.TotalAmount = 0;

                detailTable.AddWarehouseReceiptItemsWithProductViewRow(newDetailRow);


            }
            else
            {
                MessageBox.Show("Please create or select a master receipt first.");
            }
        }


        private void BtnDeleteRow_Click(object sender, EventArgs e)
        {
            if (dgReciteItem.CurrentRow != null)
            {
                dgReciteItem.Rows.Remove(dgReciteItem.CurrentRow);
            }
        }
        private void AddPartyButtonColumn()
        {
            if (!dgReciteItem.Columns.Contains("ProductSelector"))
            {
                var partyButtonColumn = new DataGridViewButtonColumn { Name = "ProductSelector", HeaderText = "Product", Text = "Select Product", UseColumnTextForButtonValue = true };
                dgReciteItem.Columns.Add(partyButtonColumn);
                }
        }
    }
}
