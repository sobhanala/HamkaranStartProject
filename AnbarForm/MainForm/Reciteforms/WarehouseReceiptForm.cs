using AnbarDomain.Tabels;
using AnbarService;
using Domain.SharedSevices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Infrastructure;


namespace AnbarForm.MainForm.Reciteforms
{
    public partial class WarehouseReceiptForm : Form
    {

        private AnbarDataSet _anbarBackingField;

        public AnbarDataSet _Anbar
        {
            get => _anbarBackingField;
            set => _anbarBackingField = value;
        }

        private AnbarDataSet.WarehouseReceiptsDataTable _receiptsTable;
        private AnbarDataSet.WarehouseReceiptItemsDataTable _receiptItemsTable;
        private readonly BindingSource _masterBindingSource = new BindingSource();
        private readonly BindingSource _detailBindingSource = new BindingSource();
        private readonly IWarehouseReceipt _warehouseReceiptService;
        private readonly IPartyManagement _partyManagement;
        private readonly IUserService _userService;
        private readonly IProductService _productService;


        public WarehouseReceiptForm(IWarehouseReceipt warehouseReceipt, IPartyManagement partyManagement, IUserService userService, IProductService productService)
        {
            _warehouseReceiptService = warehouseReceipt;
            _partyManagement = partyManagement;
            _userService = userService;
            _productService = productService;
            InitializeComponent();
            SetupDataGridView();
        }


        private void SetupDataGridView()
        {
            dataGridViewMaster.AutoGenerateColumns = false;
            dataGridViewMaster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMaster.MultiSelect = false;
            dataGridViewMaster.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMaster.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }



        private AnbarDataSet.WarehouseReceiptsRow GetSelectedReceipt()
        {
            if (dataGridViewMaster.CurrentRow == null)
            {
                MessageBox.Show("Please select a receipt first.", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }

            return (dataGridViewMaster.CurrentRow.DataBoundItem as DataRowView)?.Row as
                AnbarDataSet.WarehouseReceiptsRow;
        }

        private void WarehouseReceiptForm_Load_1(object sender, EventArgs e)
        {
            ConfigureAutoBinding();

        }

        private async void ConfigureAutoBinding()
        {
           
                _anbarBackingField =await UiSafeExecutor.ExecuteAsync( () =>  _warehouseReceiptService.GetFullDatasetAsync());

                _receiptsTable = _Anbar.WarehouseReceipts;
                _receiptItemsTable = _Anbar.WarehouseReceiptItems;

                _masterBindingSource.DataSource = _Anbar;
                _masterBindingSource.DataMember = _Anbar.WarehouseReceipts.TableName;

                var relation = _Anbar.Relations.Cast<DataRelation>()
                    .FirstOrDefault(r => r.ParentTable == _receiptsTable && r.ChildTable == _receiptItemsTable);

                if (relation == null)
                {
                    MessageBox.Show("No relation found between WarehouseReceipts and WarehouseReceiptItems.", "Binding Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _detailBindingSource.DataSource = _masterBindingSource;
                _detailBindingSource.DataMember = relation.RelationName;

                dataGridViewMaster.DataSource = _masterBindingSource;


        }

        private  void AddRecite_Click(object sender, EventArgs e)
        {
            using (var addForm = new ReciteAnbarFormincome(_userService, _warehouseReceiptService, _partyManagement, _productService))
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        _masterBindingSource.ResetBindings(false);
                        MessageBox.Show("Receipt created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ConfigureAutoBinding();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error creating receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ViewReceipt_Click(object sender, EventArgs e)
        {
            var row = GetSelectedReceipt();
            var viewForm = new ViewForm(_warehouseReceiptService);
            viewForm.LoadData(row);
            viewForm.ShowDialog();

        }
        private async void DeleteReceipt_Click(object sender, EventArgs e)
        {
            var row = GetSelectedReceipt();
            if (row == null)
            {
                MessageBox.Show("No receipt selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this receipt?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.Yes)
            {

                await UiSafeExecutor.ExecuteAsync(async () =>
                {

                    await _warehouseReceiptService.DeleteReceiptWithInventoryAsync(row);
                    MessageBox.Show("Receipt deleted successfully.", "Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ConfigureAutoBinding();
                });
            }
        }


    }
}
