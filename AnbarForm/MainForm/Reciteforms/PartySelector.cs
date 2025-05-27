using System;
using System.Data;
using System.Windows.Forms;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class PartySelector : Form
    {
        private AnbarDataSet _anbar;

        private PartiesTableAdapter _partiesTableAdapter = new PartiesTableAdapter();
        public AnbarDataSet.ProductsRow SelectedWarehouse { get; private set; }

        private BindingSource _bindingSource;
        public PartySelector()
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
            _anbar = new AnbarDataSet();
            _partiesTableAdapter.Fill(_anbar.Parties);
            _bindingSource.DataSource = _anbar.Parties;
        }


        private void Searchbox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchbox.Text.Replace("'", "''");
            _bindingSource.Filter = $"Name LIKE '%{filterText}%'";
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingSource.Current is DataRowView rowView)
            {
                SelectedWarehouse = (AnbarDataSet.ProductsRow)rowView.Row;
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
        }
}