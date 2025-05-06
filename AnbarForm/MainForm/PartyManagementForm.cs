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
using AnbarService;
using Domain.Common;
using Domain.Exceptions;

namespace AnbarForm.MainForm
{
    public partial class PartyManagementForm : Form
    {
        private readonly IPartyManagement _partyService;



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
                dataGridView1.DataSource = parties;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            }
        }




        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void PartyManagementForm_Load(object sender, EventArgs e)
        {

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
                MessageBox.Show("An unexpected error occurred during registration", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txt_name.Text = "";
                txt_email.Text = "";
            }
        }

        private void Txt_city_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }



