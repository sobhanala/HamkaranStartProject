using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using AnbarForm.MainForm.Reciteforms.selectors;
using AnbarService;
using Domain.SharedSevices;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class ReciteAnbarFormincome : Form

    {
        private AnbarDataSet _currentDataSet;


        private AnbarDataSet.WarehousesRow ReciteHeaderWarehousesRow { get; set; }
        private AnbarDataSet.PartiesRow ReciteHeaderPartiesRow { get; set; }
        private AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable viewDataTable { get; set; }


        private readonly BindingSource _masterBindingSource = new BindingSource();
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
            try
            {
                var receiptNumber = await _warehouseReceiptService.GenerateNewReceiptNumber();
                PrepareDataSetBeforeSave(receiptNumber);
                await _warehouseReceiptService.SaveReceiptWithItemsAsync(_currentDataSet);

                MessageBox.Show("Receipt saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private  void PrepareDataSetBeforeSave(string resitenum)
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

            if (cmbType.SelectedValue is byte type)
            {
                receiptRow.ReceiptStatus = type;
            }
            _currentDataSet.WarehouseReceipts.AddWarehouseReceiptsRow(receiptRow);
        }

        private void ReciteAnbarForm_Load(object sender, EventArgs e)
        {
            dgReciteItem.AutoGenerateColumns = true;
            dgReciteItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _currentDataSet = new AnbarDataSet();


            viewDataTable = _currentDataSet.WarehouseReceiptItemsWithProductView;

            _detailBindingSource.DataSource = viewDataTable;
            dgReciteItem.DataSource = _detailBindingSource;


            AddPartyButtonColumn();




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
                    MessageBox.Show($"Selected warehouse: {selectedRow.Name}");
                }
            }

        }

        private async void BtnSelectParty_Click_1(object sender, EventArgs e)
        {
            var anbar = await _partyManagement.GetPartyDataSetAsync();
            var selector = new SelectorForm<AnbarDataSet.PartiesDataTable>(anbar.Parties, "name", "Products");
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
                    MessageBox.Show($"Selected Party: {selectedRow.Name}");
                }
            }
        }


        private async void BtnSelectProduct_Click(object sender, EventArgs e)
        {
            var anbar = await _partyManagement.GetPartyDataSetAsync();
            var selector = new SelectorForm<AnbarDataSet.PartiesDataTable>(anbar.Parties, "name", "Products");
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
                    MessageBox.Show($"Selected Party: {selectedRow.Name}");
                }
            }

        }

        #endregion

        private async void DgReciteItem_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dgReciteItem.Columns[e.ColumnIndex].Name == "PartySelector")
            {
                var anbar = await _productService.GetDataSet();

                var selector = new SelectorForm<AnbarDataSet.ProductsDataTable>(anbar.Products, "name", "Select Product");
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
            viewDataTable.AddWarehouseReceiptItemsWithProductViewRow(
                ReceiptId: 0,
                ProductId: 0,
                ProductName: "",
                ProductCode: "",
                Quantity: 0,
                UnitPrice: 0
            );
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
            if (!dgReciteItem.Columns.Contains("PartySelector"))
            {
                var partyButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "PartySelector",
                    HeaderText = "Party",
                    Text = "Select Party",
                    UseColumnTextForButtonValue = true
                };

                dgReciteItem.Columns.Add(partyButtonColumn);
            }
        }

    }
}
