using AnbarDomain.Tabels;
using AnbarService;
using System;
using System.Windows.Forms;
using Infrastructure;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class ViewForm : Form
    {
        private AnbarDataSet.WarehouseReceiptsRow _receiptRow;
        private readonly IWarehouseReceipt _warehouseReceiptService;

        public ViewForm(IWarehouseReceipt warehouseReceiptService)
        {
            _warehouseReceiptService = warehouseReceiptService;
            InitializeComponent();
            SetupGrid();
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
        }

        public async void LoadData(AnbarDataSet.WarehouseReceiptsRow receiptRow)
        {
            _receiptRow = receiptRow;
            var masterTable = receiptRow.Table.Clone();
            masterTable.ImportRow(receiptRow);
            masterGrid.DataSource = masterTable;

            var viewTable = await UiSafeExecutor.ExecuteAsync(()=>  _warehouseReceiptService.FillByReceiptIdWithProductInfo(receiptId: _receiptRow.Id)); 

            detailGrid.DataSource = viewTable;
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {

        }
    }
}
