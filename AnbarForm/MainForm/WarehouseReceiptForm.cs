using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using AnbarService;
using Domain.SharedSevices;

//TODO fix the view  detail order ==> on left click 
//TODO view the detailed version 
//TODO and show the abstract  version on detail 
//TODO on double click show detailed version of the recite 

namespace AnbarForm.MainForm
{
    public partial class WarehouseReceiptForm : Form
    {

        private AnbarDataSet _anbarBackingField;

        public AnbarDataSet _Anbar
        {
            get => _anbarBackingField;
            set => _anbarBackingField = value;
        }

        private PartiesTableAdapter _partyAdapter = new PartiesTableAdapter();

        private WarehousesTableAdapter _warehouseAdapter= new WarehousesTableAdapter();

        private AnbarDataSet.WarehouseReceiptsDataTable _receiptsTable;
        private AnbarDataSet.WarehouseReceiptItemsDataTable _receiptItemsTable;
        private readonly BindingSource _masterBindingSource = new BindingSource();
        private readonly BindingSource _detailBindingSource = new BindingSource();
        private readonly IWarehouseReceipt _warehouseReceiptService;
        private readonly IPartyManagement _partyManagement;
        private readonly IUserService _userService;
        

        public WarehouseReceiptForm(IWarehouseReceipt warehouseReceipt, IPartyManagement partyManagement, IUserService userService)
        {
            _warehouseReceiptService = warehouseReceipt;
            _partyManagement = partyManagement;
            _userService = userService;
            InitializeComponent();
            SetupContextMenu();
            SetupDataGridView();
            SetupPanel();

        }

        private void SetupPanel()
        {
            panel_Details.Visible = false; 
            panel_Details.Dock = DockStyle.None;
        }

        private void ShowPanelDetails()
        {
            panel_Details.Visible = true;

        }

        private void SetupDataGridView()
        {
            dataGridViewMaster.AutoGenerateColumns = false;
            dataGridViewMaster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMaster.MultiSelect = false;

            
        }

        private void SetupContextMenu()
        {

            EditReceipt.Click += EditMenuItem_Click;

            DeleteReceipt.Click += DeleteMenuItem_Click;

            toolStripMenuItem1.Click += ViewDetailsMenuItem_Click;

            dataGridViewMaster.ContextMenuStrip = contextMenuStrip1;
        }



        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            var selectedRow = GetSelectedReceipt();
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a receipt to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _partyAdapter.Fill(_Anbar.Parties);
            _warehouseAdapter.Fill(_Anbar.Warehouses);

            using (var editForm = new EditReciteForm(_Anbar, selectedRow, _warehouseReceiptService))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    _masterBindingSource.ResetBindings(false);
                    _detailBindingSource.ResetBindings(false);
                }
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            var receiptRow = GetSelectedReceipt();

            if (MessageBox.Show(
                    $"Are you sure you want to delete Receipt #{receiptRow.ReceiptNumber} and all its items?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var childRows = receiptRow.GetWarehouseReceiptItemsRows();
                foreach (var childRow in childRows)
                {
                    childRow.Delete();
                }

                receiptRow.Delete();

                _masterBindingSource.ResetBindings(false);
                _detailBindingSource.ResetBindings(false);

                _masterBindingSource.EndEdit();
                _detailBindingSource.EndEdit();


                _warehouseReceiptService.SaveChangesAsync(_Anbar);


            }
        }

        private  void ViewDetailsMenuItem_Click(object sender, EventArgs e)
        {
            var receiptRow = GetSelectedReceipt();
            if (receiptRow == null) return;
             DisplayReceiptDetails(receiptRow);
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
            using (var addForm = new AddReceiptForm(_Anbar.Warehouses.Copy() as AnbarDataSet.WarehousesDataTable,
                       _Anbar.Parties.Copy() as AnbarDataSet.PartiesDataTable))
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _warehouseReceiptService.CreateWarehouseReceiptAsync(
                            _Anbar,
                            addForm.SelectedWarehouseId,
                            addForm.SelectedPartyId,
                            addForm.Type
                            ,addForm.Date);

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


        private async void DisplayReceiptDetails(AnbarDataSet.WarehouseReceiptsRow receipt)
        {
            await LoadPartyInfo(receipt.PartyId);
            LoadWarehouseInfo(receipt.WarehouseId);

            int? createdBy = receipt.IsCreatedByNull() ? null : (int?)receipt.CreatedBy;
            int? updatedBy = receipt.IsUpdatedByNull() ? null : (int?)receipt.UpdatedBy;

            await LoadUserInfo(createdBy, updatedBy);
            FillReceiptLabels(receipt);

            ShowPanelDetails();
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private async Task LoadPartyInfo(int partyId)
        {
            var party = await _partyManagement.GetPartyById(partyId);
            labelPartyName.Text = $"Party Name: {party?.Name ?? "Unknown Party"}";
        }


        private void LoadWarehouseInfo(int warehouseId)
        {
            _warehouseAdapter.Fill(_Anbar.Warehouses);
            var warehouseRow = _Anbar.Warehouses.FirstOrDefault(w => w.Id == warehouseId);
            lblWareHouseName.Text = $"Warehouse Name: {warehouseRow?.Name ?? "Unknown Warehouse"}";
        }


        private async Task LoadUserInfo(int? createdBy, int? updatedBy)
        {
            if (createdBy.HasValue)
            {
                var creator = await _userService.GetUserById(createdBy.Value);
                lblCreatedBy.Text = $"Created By: {creator?.Username ?? $"ID: {createdBy.Value}"}";
            }
            else
            {
                lblCreatedBy.Text = "Created By: Unknown";
            }

            if (updatedBy.HasValue)
            {
                var updater = await _userService.GetUserById(updatedBy.Value);
                lblUpdateBy.Text = $"Updated By: {updater?.Username ?? $"ID: {updatedBy.Value}"}";
            }
            else
            {
                lblUpdateBy.Text = "Updated By: Not updated";
            }
        }

        private void FillReceiptLabels(AnbarDataSet.WarehouseReceiptsRow receipt)
        {
            lblDiscount.Text = $"Discount: {(receipt.IsDiscountNull() ? "0" : receipt.Discount.ToString("N0"))}";
            lblTransportCost.Text = $"Transport Cost: {(receipt.IsTransportCostNull() ? "0" : receipt.TransportCost.ToString("N0"))}";
            lblTotalCost.Text = $"Total Cost: {(receipt.IsTotalAmountNull() ? "0" : receipt.TotalAmount.ToString("N0"))}";

            lblReciteDate.Text = $"Receipt Date: {(receipt.IsReceiptDateNull() ? "N/A" : receipt.ReceiptDate.ToShortDateString())}";
            lblCreatedAt.Text = $"Created At: {(receipt.IsCreatedAtNull() ? "N/A" : receipt.CreatedAt.ToShortDateString())}";
            lblUpdatedAt.Text = $"Updated At: {(receipt.IsUpdatedAtNull() ? "N/A" : receipt.UpdatedAt.ToShortDateString())}";

            lblType.Text = $"Type: {((ReciteType)receipt.ReceiptStatus).ToString()}";
        }

        private void Btn_PanelClose_Click(object sender, EventArgs e)
        {
            SetupPanel();
        }
    }
}
