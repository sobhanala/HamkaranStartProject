using System;
using System.Windows.Forms;
using AnbarDomain.Products;
using AnbarDomain.Tabels;
using AnbarService;

namespace AnbarForm.MainForm
{
    public partial class ProductManagementForm : Form
    {
        private readonly IPartyManagement _partyService;//TODO IProducts service 

        public AnbarDataSet AnbarDataSet1
        {
            get => anbarDataSet1;
            set => anbarDataSet1 = value;
        }
        public ProductManagementForm(IPartyManagement partyService)
        {
            _partyService = partyService;
            InitializeComponent();
            SetupDataGrid();
            LoadData();

        }

        private void Btn_save_all_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private async void LoadData()
        {
            try
            {
                AnbarDataSet1.Products.Clear();

                //AnbarDataSet1 = await _productService.GetProductDataSetAsync();

                // Ensure DataGridView knows about data changes
                AnbarDataSet1.Products.AcceptChanges();

                // Rebind data source if needed
                dataGridView1.DataSource = AnbarDataSet1.Products;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddDataGridColumns()
        {
            dataGridView1.Columns.Clear();

            var idColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "Id",
                Visible = false
            };
            dataGridView1.Columns.Add(idColumn);

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Product Name",
                Name = "Name",
                Width = 200
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Description",
                Name = "Description",
                Width = 250
            });

            var unitColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "UnitOfMeasure",
                HeaderText = "Unit",
                Name = "UnitOfMeasure",
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };

            // ReSharper disable once CoVariantArrayConversion
            unitColumn.Items.AddRange(Enum.GetNames(typeof(Unit))); 
            dataGridView1.Columns.Add(unitColumn);

            // Cost Price
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CostPrice",
                HeaderText = "Cost Price",
                Name = "CostPrice",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            // Selling Price
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SellingPrice",
                HeaderText = "Selling Price",
                Name = "SellingPrice",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            // Weight
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Weight",
                HeaderText = "Weight",
                Name = "Weight",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedAt",
                HeaderText = "Created At",
                Name = "CreatedAt",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
            });

            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                Text = "Delete",
                Name = "delete",
                UseColumnTextForButtonValue = true,
                Width = 60
            });
        }
        private void SetupDataGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;

            AddDataGridColumns();

            dataGridView1.DataSource = AnbarDataSet1.Products;
        }
    }
}
