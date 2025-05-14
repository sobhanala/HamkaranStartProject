using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AnbarDomain.Products;
using AnbarDomain.Tabels;
using AnbarService;
using Domain.Exceptions;

namespace AnbarForm.MainForm
{
    public partial class ProductManagementForm : Form
    {
        private readonly IProductService _productService; //TODO IProducts service 

        public AnbarDataSet AnbarDataSet1
        {
            get => anbarDataSet1;
            set => anbarDataSet1 = value;
        }

        public ProductManagementForm(IProductService productService)
        {
            _productService = productService;
            InitializeComponent();
            SetupDataGrid();
            LoadData();
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private async void LoadData()
        {
            try
            {
                AnbarDataSet1.Products.Clear();

                AnbarDataSet1 = await _productService.GetDataSet();
                AnbarDataSet1.Products.AcceptChanges();


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

            unitColumn.DataSource = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select
                (u => new { Name = u.ToString(), Value = (int)u }).ToList();

            unitColumn.DisplayMember = "Name";
            unitColumn.ValueMember = "Value";

            dataGridView1.Columns.Add(unitColumn);

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CostPrice",
                HeaderText = "Cost Price",
                Name = "CostPrice",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SellingPrice",
                HeaderText = "Selling Price",
                Name = "SellingPrice",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

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

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductCode",
                HeaderText = "Product Code",
                Name = "ProductCode",
                Width = 130,
                ReadOnly = true
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

        private void LogChanges()
        {
            var modifiedRows = AnbarDataSet1.Products.Select("", "", DataViewRowState.ModifiedCurrent);
            foreach (DataRow row in modifiedRows)
            {
                Console.WriteLine($"Modified Row: {row["Id"]} - {row["Name"]}");
            }


            var deletedRows = AnbarDataSet1.Products.Select("", "", DataViewRowState.Deleted);
            foreach (DataRow row in deletedRows)
            {
                Console.WriteLine($"Deleted Row: {row["Id", DataRowVersion.Original]}");
            }

            var addedRows = AnbarDataSet1.Products.Select("", "", DataViewRowState.Added);
            foreach (DataRow row in addedRows)
            {
                Console.WriteLine($"Added Row: {row["Name"]}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "delete")
            {
                var confirm = MessageBox.Show("Do you want to mark this row for deletion?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    var row = dataGridView1.Rows[e.RowIndex];

                    if (row.DataBoundItem is DataRowView dataRowView)
                    {
                        dataRowView.Row.Delete();
                    }
                }

            }
        }

        private async void Btn_save_all_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();

                var changedTable = (AnbarDataSet.ProductsDataTable)AnbarDataSet1.Products.GetChanges();

                if (changedTable == null || changedTable.Rows.Count == 0)
                {
                    MessageBox.Show("No changes to save.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                LogChanges();

                var changeDataTable = await _productService.SetProductValues(changedTable);
                await _productService.SaveAllChanges(changeDataTable);

                AnbarDataSet1.AcceptChanges();


                MessageBox.Show(" All Changes Saved.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.UserFriendlyMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.UserFriendlyMessage, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

    }
}
