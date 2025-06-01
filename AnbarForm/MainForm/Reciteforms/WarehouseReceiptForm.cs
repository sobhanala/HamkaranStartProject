using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using AnbarPersitence.Newway;
using AnbarService;
using Domain.SharedSevices;



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

        private EnhancedWarehouseReceiptsTableAdapter _warehouseaAdapter;

        private PartiesTableAdapter _partyAdapter = new PartiesTableAdapter();

        private WarehousesTableAdapter _warehouseAdapter= new WarehousesTableAdapter();

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
            //SetupPanel();
        }

        //private void SetupPanel()
        //{
        //    panel_Details.Visible = false; 
        //    panel_Details.Dock = DockStyle.None;
        //}

        //private void ShowPanelDetails()
        //{
        //    panel_Details.Visible = true;

        //}

        private void SetupDataGridView()
        {
            dataGridViewMaster.AutoGenerateColumns = false;
            dataGridViewMaster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMaster.MultiSelect = false;

            
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

        private  void WarehouseReceiptForm_Load_1(object sender, EventArgs e)
        {
             ConfigureAutoBinding();

        }

        private async void ConfigureAutoBinding()
        {
            try
            {

                _anbarBackingField = await _warehouseReceiptService.GetFullDatasetAsync();

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
            catch (Exception e)
            {
                MessageBox.Show($"Error loading data: {e.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void AddRecite_Click(object sender, EventArgs e)
        {
            _warehouseAdapter.Fill(_Anbar.Warehouses);
            _partyAdapter.Fill(_Anbar.Parties);
            using (var addForm = new ReciteAnbarFormincome(_userService,_warehouseReceiptService,_partyManagement,_productService))
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        _masterBindingSource.ResetBindings(false);
                        MessageBox.Show("Receipt created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void DeleteReceipt_Click(object sender, EventArgs e)
        {

        }

    }
}
