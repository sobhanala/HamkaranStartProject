using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using System;
using System.Data;
using System.Windows.Forms;
using AnbarDomain.Tabels.warhousesTableAdapters;

namespace AnbarForm.MainForm.Reciteforms.selectors
{
    public partial class WarehouseSelectorForm : Form
    {
        private warhouses _warhouse;

        private WarehousesTableAdapter _warehouseAdapter = new WarehousesTableAdapter();
        public warhouses.WarehousesRow SelectedWarehouse { get; private set; }

        private BindingSource _bindingSource = new BindingSource();
        public WarehouseSelectorForm()
        {
            InitializeComponent();
            InittializeTheDs();
            InitializeControls();

        }

        private void InitializeControls()
        {
            dataGridView1.DataSource = _bindingSource;

        }

        private void InittializeTheDs()
        {
            _warhouse = new warhouses();
            _warehouseAdapter.Fill(_warhouse.Warehouses);
            _bindingSource.DataSource = _warhouse.Warehouses;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingSource.Current is DataRowView rowView)
            {
                SelectedWarehouse = (warhouses.WarehousesRow)rowView.Row;
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
        }

        private void Textbox_TextChanged(object sender, EventArgs e)
        {
            string filterText = serachbox.Text.Replace("'", "''");
            _bindingSource.Filter = $"Name LIKE '%{filterText}%'";
        }
    }
}
