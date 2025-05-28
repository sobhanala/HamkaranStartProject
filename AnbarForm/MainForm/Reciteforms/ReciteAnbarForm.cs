using System;
using System.Drawing;
using System.Windows.Forms;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using AnbarForm.MainForm.Reciteforms.selectors;
using AnbarService;
using Domain.SharedSevices;

namespace AnbarForm.MainForm
{
    public partial class ReciteAnbarFormincome : Form

    {
        private AnbarDataSet _anbarBackingField;

        public AnbarDataSet _anbar
        {
            get => _anbarBackingField;
            set => _anbarBackingField = value;
        }

        private AnbarDataSet.WarehousesRow ReciteHeaderWarehousesRow { get; set; }
        private AnbarDataSet.PartiesRow ReciteHeaderPartiesRow { get; set; }
        private AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable viewDataTable;


        private readonly BindingSource _masterBindingSource = new BindingSource();
        private readonly BindingSource _detailBindingSource = new BindingSource();
        private readonly WarehousesTableAdapter _warehousesTableAdapter = new WarehousesTableAdapter();

        private readonly IWarehouseReceipt _warehouseReceiptService;
        private readonly IPartyManagement _partyManagement;
        private readonly IUserService _userService;

        public ReciteAnbarFormincome(IUserService userService, IWarehouseReceipt warehouseReceiptService,
            IPartyManagement partyManagement)
        {
            _userService = userService;
            _warehouseReceiptService = warehouseReceiptService;
            _partyManagement = partyManagement;
            InitializeComponent();

        }





        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {

        }

        private void ReciteAnbarForm_Load(object sender, EventArgs e)
        {
            dgReciteItem.AutoGenerateColumns = true;
            dgReciteItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            viewDataTable = new AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable();

            _detailBindingSource.DataSource = viewDataTable;
            dgReciteItem.DataSource = _detailBindingSource;

            viewDataTable.AddWarehouseReceiptItemsWithProductViewRow(
                ReceiptId:0,
                ProductId:0,
                ProductName:"",
                ProductCode:"",
                Quantity:0,
                UnitPrice:0
            );

            ReplaceProductIdWithButtonColumn();




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

            if (e.RowIndex >= 0 && dgReciteItem.Columns[e.ColumnIndex].Name == "ProductId")
            {
                var anbar = await _partyManagement.GetPartyDataSetAsync();

                var selector = new SelectorForm<AnbarDataSet.PartiesDataTable>(anbar.Parties, "name", "Select Product");
                selector.StartPosition = FormStartPosition.Manual;

                var gridLocation = dgReciteItem.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var screenPoint = dgReciteItem.PointToScreen(gridLocation.Location);
                selector.Location = new Point(screenPoint.X, screenPoint.Y + gridLocation.Height);

                if (selector.ShowDialog() == DialogResult.OK)
                {
                    var selectedRow = (AnbarDataSet.PartiesRow)selector.SelectedRow;
                    if (selectedRow != null)
                    {
                        dgReciteItem.Rows[e.RowIndex].Cells["ProductName"].Value = selectedRow.Name;
                        dgReciteItem.Rows[e.RowIndex].Cells["ProductId"].Value = selectedRow.Id;
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
        private void ReplaceProductIdWithButtonColumn()
        {
            if (dgReciteItem.Columns.Contains("ProductId"))
            {
                int columnIndex = dgReciteItem.Columns["ProductId"].Index;

                dgReciteItem.Columns.Remove("ProductId");

                var buttonColumn = new DataGridViewButtonColumn
                {
                    Name = "ProductId",
                    HeaderText = "Product",
                    Text = "Select",
                    UseColumnTextForButtonValue = true
                };

                dgReciteItem.Columns.Insert(columnIndex, buttonColumn);
            }
        }

    }
}
