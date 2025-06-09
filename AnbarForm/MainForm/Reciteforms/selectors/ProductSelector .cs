using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using System.Data;
using System.Windows.Forms;
using AnbarDomain.Tabels.ProductDatasetTableAdapters;

namespace AnbarForm.MainForm.Reciteforms.selectors
{
    public partial class ProductSelector : Form
    {
        private ProductDataset _anbar = new ProductDataset();
        private ProductsTableAdapter _productsAdapter = new ProductsTableAdapter();
        private BindingSource _bindingSource = new BindingSource();

        public ProductDataset.ProductsRow SelectedProduct { get; private set; }
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
                SelectedProduct = (ProductDataset.ProductsRow)view.Row;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
