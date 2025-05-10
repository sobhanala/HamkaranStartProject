using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnbarDomain.Partys;
using AnbarPersitence;
using AnbarService;
using Domain.Common;
using Domain.Exceptions;

namespace AnbarForm.MainForm
{
    public partial class PartyManagementForm : Form
    {
        private readonly IPartyManagement _partyService;

        public AnbarDataSet AnbarDataSet1
        {
            get => anbarDataSet1;
            set => anbarDataSet1 = value;
        }

        public PartyManagementForm(IPartyManagement partyService)
        {
            _partyService = partyService;
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var parties = await _partyService.GetAllParties();
                AnbarDataSet1.Parties.Clear();

                foreach (var party in parties)
                {
                    AnbarDataSet1.Parties.Rows.Add(
                        party.Id,
                        party.Name,
                        party.Email,
                        party.Address?.Street,
                        party.Address?.City,
                        party.Address?.PostalCode,
                        party.Address?.Country,
                        party.IsActive,
                        party.PartyType,
                        party.CreatedAt
                    );
                }

                // Important: Mark all rows as unchanged after loading
                AnbarDataSet1.Parties.AcceptChanges();

                dataGridView1.DataSource = AnbarDataSet1.Parties;
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

                _partyService.AddParty(party);

                MessageBox.Show("Party saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.UserFriendlyMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.UserFriendlyMessage, "Authentication Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred during registration: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
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

                await _partyService.SaveAllChanges(changedTable);

                AnbarDataSet1.Parties.AcceptChanges();

                MessageBox.Show("All changes saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload data to ensure consistency
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogChanges()
        {
            // Log modified rows
            var modifiedRows = AnbarDataSet1.Parties.Select("", "", DataViewRowState.ModifiedCurrent);
            foreach (DataRow row in modifiedRows)
            {
                Console.WriteLine($"Modified Row: {row["Id"]} - {row["Name"]}");
            }

            // Log deleted rows
            var deletedRows = AnbarDataSet1.Parties.Select("", "", DataViewRowState.Deleted);
            foreach (DataRow row in deletedRows)
            {
                Console.WriteLine($"Deleted Row: {row["Id", DataRowVersion.Original]}");
            }

            // Log added rows
            var addedRows = AnbarDataSet1.Parties.Select("", "", DataViewRowState.Added);
            foreach (DataRow row in addedRows)
            {
                Console.WriteLine($"Added Row: {row["Name"]}");
            }
        }

        // Other event handlers remain mostly unchanged
        private void TextBox3_TextChanged(object sender, EventArgs e) { }
        private void PartyManagementForm_Load(object sender, EventArgs e) { }
        private void Txt_city_TextChanged(object sender, EventArgs e) { }
        private void Txt_name_TextChanged(object sender, EventArgs e) { }
        private void Tb_country_TextChanged(object sender, EventArgs e) { }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}