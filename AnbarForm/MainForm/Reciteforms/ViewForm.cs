using AnbarDomain.Tabels;
using System;
using System.Windows.Forms;
using AnbarDomain.Orders;
using AnbarDomain.Partys;
using AnbarService.Interfaces;
using Infrastructure;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class ViewForm : Form
    {
        private AnbarDataSet.view_WarehouseReceiptsRow _receiptRow;
        private readonly IWarehouseReceipt _warehouseReceiptService;
        private BindingSource _masterBindingSource = new BindingSource();


        public ViewForm(IWarehouseReceipt warehouseReceiptService)
        {
            _warehouseReceiptService = warehouseReceiptService;
            InitializeComponent();
            SetupGrid();
            ConfigBindingSource();
        }

        private void ConfigBindingSource()
        {
        }

        private void SetupGrid()
        {
            detailGrid.AutoGenerateColumns = true;
            masterGrid.AutoGenerateColumns = true;
            masterGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            detailGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            masterGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            detailGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            masterGrid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            detailGrid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            masterGrid.CellFormatting += masterGrid_CellFormatting;

        }

        private void masterGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (masterGrid.Columns[e.ColumnIndex].Name == "ReceiptStatus" && e.Value is byte byteValue)
            {
                e.Value = Enum.GetName(typeof(ReciteType), byteValue);
                e.FormattingApplied = true;
            }
            if (masterGrid.Columns[e.ColumnIndex].Name == "PartyType" && e.Value is int intValue)
            {
                e.Value = Enum.GetName(typeof(PartyType), intValue);
                e.FormattingApplied = true;
            }
        }

        public async void LoadData(AnbarDataSet.view_WarehouseReceiptsRow receiptRow)
        {
            _receiptRow = receiptRow;
            var masterTable = receiptRow.Table.Clone();
            masterTable.ImportRow(receiptRow);

            _masterBindingSource.DataSource = masterTable;
            masterGrid.DataSource = _masterBindingSource;

            var viewTable = await UiSafeExecutor.ExecuteAsync(()=>  _warehouseReceiptService.FetchDetailsByMasterIdAsync(receiptId: _receiptRow.Id)); 
            detailGrid.DataSource = viewTable;

        }

        private void ViewForm_Load(object sender, EventArgs e)
        {

        }
    }
}
