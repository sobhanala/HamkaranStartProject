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
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class ProductSelector : Form
    {
        private AnbarDataSet _anbar = new AnbarDataSet();
        private ProductsTableAdapter _productsAdapter = new ProductsTableAdapter();
        private BindingSource _bindingSource = new BindingSource();

        public AnbarDataSet.ProductsRow SelectedProduct { get; private set; }
        public ProductSelector()
        {
            InitializeComponent();
            _productsAdapter.Fill(_anbar.Products);
            _bindingSource.DataSource = _anbar.Products;
            dataGridView1.DataSource = _bindingSource;
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && _bindingSource.Current is DataRowView view)
            {
                SelectedProduct = (AnbarDataSet.ProductsRow)view.Row;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
