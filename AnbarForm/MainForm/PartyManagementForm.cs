using AnbarDomain.Partys;
using AnbarDomain.Tabels;
using AnbarService;
using Domain.Common;
using Domain.Exceptions;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AnbarService.Interfaces;
using Infrastructure;

namespace AnbarForm.MainForm
{
    public partial class PartyManagementForm : Form
    {
        private readonly IPartyManagement _partyService;

        public warhouses AnbarDataSet1 = new warhouses();


        public PartyManagementForm(IPartyManagement partyService)
        {
            _partyService = partyService;
            InitializeComponent();
            LoadData();

        }

        private void ConfigDataGrid(){
            if (dataGridView1.Columns.Contains("Id"))
            {
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Id"].Visible = false;

            }
        }

        private async void LoadData()
        {
            try
            {
                AnbarDataSet1.Parties.Clear();
                AnbarDataSet1 = await UiSafeExecutor.ExecuteAsync(() => _partyService.GetPartyDataSetAsync());


                AnbarDataSet1.Parties.AcceptChanges();

                dataGridView1.DataSource = AnbarDataSet1.Parties;
                ConfigDataGrid();

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading data: {e.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_name.Text))
            {
                MessageBox.Show("Please enter a party name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var party = new Party
                {
                    PartyType = 0,
                    Name = txt_name.Text,
                    Email = txt_email.Text,
                    Address = new Address(tb_street.Text, txt_city.Text, txt_postalCode.Text, tb_country.Text),
                    CreatedAt = DateTime.Now
                };

                UiSafeExecutor.Execute(() => _partyService.AddParty(party));

                MessageBox.Show("Party saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
            }

            finally
            {
                ClearInputFields();
            }
        }

        private void ClearInputFields()
        {
            txt_name.Text = "";
            txt_email.Text = "";
            tb_street.Text = "";
            txt_city.Text = "";
            txt_postalCode.Text = "";
            tb_country.Text = "";
        }

        private async void Btn_save_all_Click(object sender, EventArgs e)
        {
        
                dataGridView1.EndEdit();

                DataTable changedTable = AnbarDataSet1.Parties.GetChanges();

                if (changedTable == null || changedTable.Rows.Count == 0)
                {
                    MessageBox.Show("No changes to save.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                LogChanges();

                var err = await UiSafeExecutor.ExecuteAsync(
                    async () => await _partyService.SaveAllChanges(changedTable));
                if (!err)
                {
                    return;
                }

                AnbarDataSet1.Parties.AcceptChanges();

                MessageBox.Show("All changes saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();

        }

        private void LogChanges()
        {
            var modifiedRows = AnbarDataSet1.Parties.Select("", "", DataViewRowState.ModifiedCurrent);
            foreach (DataRow row in modifiedRows)
            {
                Console.WriteLine($"Modified Row: {row["Id"]} - {row["Name"]}");
            }

            var deletedRows = AnbarDataSet1.Parties.Select("", "", DataViewRowState.Deleted);
            foreach (DataRow row in deletedRows)
            {
                Console.WriteLine($"Deleted Row: {row["Id", DataRowVersion.Original]}");
            }

            var addedRows = AnbarDataSet1.Parties.Select("", "", DataViewRowState.Added);
            foreach (DataRow row in addedRows)
            {
                Console.WriteLine($"Added Row: {row["Name"]}");
            }
        }

        // Other event handlers remain mostly unchanged
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void PartyManagementForm_Load(object sender, EventArgs e)
        {
        }

        private void Txt_city_TextChanged(object sender, EventArgs e)
        {
        }

        private void Txt_name_TextChanged(object sender, EventArgs e)
        {
        }

        private void Tb_country_TextChanged(object sender, EventArgs e)
        {
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
    }
}