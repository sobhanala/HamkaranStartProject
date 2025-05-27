using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnbarDomain.Tabels;
using AnbarForm.MainForm.Reciteforms;
using AnbarPersitence.Newway;
using AnbarService;
using Domain.SharedSevices;

namespace AnbarForm.MainForm
{
    //TODO in sanad anbar voroude yani in ha be mojodi ezafe mikonan  be inventory va in 
    public partial class ReciteAnbarFormincome : Form

    {
        private AnbarDataSet _anbarBackingField;

        public AnbarDataSet _anbar
        {
            get => _anbarBackingField;
            set => _anbarBackingField = value;
        }

        private EnhancedWarehouseReceiptsTableAdapter _warehouseaAdapter;

        private AnbarDataSet.WarehouseReceiptsDataTable _receiptsTable;
        private AnbarDataSet.WarehouseReceiptItemsDataTable _receiptItemsTable;
        private readonly BindingSource _masterBindingSource = new BindingSource();
        private readonly BindingSource _detailBindingSource = new BindingSource();
        private readonly IWarehouseReceipt _warehouseReceiptService;
        private readonly IPartyManagement _partyManagement;
        private readonly IUserService _userService;
        public ReciteAnbarFormincome()
        {
            InitializeComponent();
            AddControlNestedTableLayoutPanel();
            AddControlFlowLayoutPanel();
            SetupEditableGrid();
        }
        private void SetupEditableGrid()
        {
            dgReciteItem.Columns.Clear();

            var productNameColumn = new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product",
                ReadOnly = true,
                DataPropertyName = "ProductName"
            };
            dgReciteItem.Columns.Add(productNameColumn);


            var productIdColumn = new DataGridViewTextBoxColumn
            {
                Name = "ProductId",
                HeaderText = "ProductId",
                Visible = false,
                DataPropertyName = "ProductId"
            };

            dgReciteItem.Columns.Add(productIdColumn);

            dgReciteItem.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantity",
                DataPropertyName = "Quantity",

            });

            dgReciteItem.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                HeaderText = "Unit Price",
                DataPropertyName = "UnitPrice",


            });

            dgReciteItem.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                ReadOnly = true
            });

            var deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };
            dgReciteItem.Columns.Add(deleteColumn);
        }


        private void AddControlFlowLayoutPanel()
        {
            Panel panelProduct = new Panel { AutoSize = true };
            panelProduct.Controls.Add(new Label
            {
                Text = "Product:",
                Dock = DockStyle.Top,
                AutoSize = true
            });
            panelProduct.Controls.Add(new ComboBox
            {
                Name = "cmbProduct",
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            flowLayoutPanel.Controls.Add(panelProduct);

            Panel panelQty = new Panel { AutoSize = true };
            panelQty.Controls.Add(new Label
            {
                Text = "Qty:",
                Dock = DockStyle.Top,
                AutoSize = true
            });
            panelQty.Controls.Add(new NumericUpDown
            {
                Name = "numQty",
                Width = 60,
                Minimum = 1,
                Maximum = 1000
            });
            flowLayoutPanel.Controls.Add(panelQty);

            Panel panelPrice = new Panel { AutoSize = true };
            panelPrice.Controls.Add(new Label
            {
                Text = "Price:",
                Dock = DockStyle.Top,
                AutoSize = true
            });
            panelPrice.Controls.Add(new NumericUpDown
            {
                Name = "numPrice",
                Width = 80,
                DecimalPlaces = 2,
                Minimum = 0,
                Maximum = 100000
            });
            flowLayoutPanel.Controls.Add(panelPrice);


        }

        private void AddControlNestedTableLayoutPanel()
        {
            var txtWarehouse = new TextBox { Name = "txtWarehouse", ReadOnly = true, Dock = DockStyle.Fill };
            var btnSelectWarehouse = new Button { Text = "...", Width = 30, Dock = DockStyle.Right };
            btnSelectWarehouse.Click += BtnSelectWarehouse_Click;//TODO 

            var pnlWarehouse = new Panel { Dock = DockStyle.Fill };
            pnlWarehouse.Controls.Add(txtWarehouse);
            pnlWarehouse.Controls.Add(btnSelectWarehouse);
            nestedTableLayoutPanel.Controls.Add(pnlWarehouse, 0, 0);


            // Party Selector
            var txtParty = new TextBox { Name = "txtParty", ReadOnly = true, Dock = DockStyle.Fill };
            var btnSelectParty = new Button { Text = "...", Width = 30, Dock = DockStyle.Right };
            btnSelectParty.Click += BtnSelectParty_Click;

            var pnlParty = new Panel { Dock = DockStyle.Fill };
            pnlParty.Controls.Add(txtParty);
            pnlParty.Controls.Add(btnSelectParty);
            nestedTableLayoutPanel.Controls.Add(pnlParty, 1, 0);



            nestedTableLayoutPanel.Controls.Add(new Label { Name = "Date", Text = "Date:", Dock = DockStyle.Top, Anchor = AnchorStyles.Left, AutoSize = true }, 2, 0);
            nestedTableLayoutPanel.Controls.Add(new DateTimePicker() { Name = "tpDate", Text = "Partys",Format = DateTimePickerFormat.Short,Value = DateTime.Now,
                Dock = DockStyle.Fill, Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top, AutoSize = true }, 2, 0);

            nestedTableLayoutPanel.Controls.Add(new Label { Name = "lbltype", Text = "Type:", Dock = DockStyle.Top, Anchor = AnchorStyles.Left, AutoSize = true }, 0, 1);
            nestedTableLayoutPanel.Controls.Add(new ComboBox() { Name = "cmbType", Text = "Types", Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top, AutoSize = true }, 0, 1);

            nestedTableLayoutPanel.Controls.Add(new Label { Name = "lblDiscount", Text = "Discount:", Dock = DockStyle.Top, Anchor = AnchorStyles.Left, AutoSize = true }, 2, 1);
            nestedTableLayoutPanel.Controls.Add(new NumericUpDown() { Name = "numDiscount", Text = "Discount",
                Dock = DockStyle.Fill, Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top, AutoSize = true }, 2, 1);

            nestedTableLayoutPanel.Controls.Add(new Label { Name = "lblCost", Text = "Cost:", Dock = DockStyle.Top, Anchor = AnchorStyles.Left, AutoSize = true }, 1, 1);
            nestedTableLayoutPanel.Controls.Add(new NumericUpDown() { Name = "numCost", Text = "Cost:", Dock = DockStyle.Fill, Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top, AutoSize = true }, 1, 1);

        }

        private void BtnSelectWarehouse_Click(object sender, EventArgs e)
        {
            var selector = new WarehouseSelectorForm();
            selector.StartPosition = FormStartPosition.Manual;

            var btn = sender as Button;
            var buttonPosition = btn.PointToScreen(Point.Empty);

            selector.Location = new Point(buttonPosition.X, buttonPosition.Y + btn.Height);

            selector.Deactivate += (s, args) => selector.Close();

            selector.Show(); 
        }

        private void BtnSelectParty_Click(object sender, EventArgs e)
        {
            var selector = new PartySelector();
            selector.StartPosition = FormStartPosition.Manual;

            var btn = sender as Button;
            var buttonPosition = btn.PointToScreen(Point.Empty);

            selector.Location = new Point(buttonPosition.X, buttonPosition.Y + btn.Height);

            selector.Deactivate += (s, args) => selector.Close();

            selector.Show();
        }


        private void DgReciteItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {

        }

        private void ReciteAnbarForm_Load(object sender, EventArgs e)
        {

        }
    }
}
