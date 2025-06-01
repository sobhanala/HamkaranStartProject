﻿using System;
using System.Linq;
using System.Windows.Forms;
using AnbarDomain.Orders;
using AnbarDomain.Tabels;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class AddReceiptForm : Form
    {
        public int SelectedWarehouseId => (int)cmbwarehouse.SelectedValue;
        public int SelectedPartyId => (int)cmbParty.SelectedValue;
        public decimal  Disount;
        public decimal TransportCost;
        public byte Type;
        public DateTime Date;

        public AddReceiptForm(AnbarDataSet.WarehousesDataTable warehouses, AnbarDataSet.PartiesDataTable parties)
        {
            InitializeComponent();
            ConfigureComboBox(warehouses,parties);
            ConfigureDate();

        }

        private void Btn_ok_Click(object sender, EventArgs e)
        {


            if (cmbwarehouse.SelectedItem == null || cmbParty.SelectedItem == null || string.IsNullOrWhiteSpace(tb_Discount.Text) || string.IsNullOrWhiteSpace(tb_TransportCost.Text))
            {
                MessageBox.Show("Please select both a warehouse and a party.");
                return;
            }
            if (decimal.TryParse(tb_TransportCost.Text, out TransportCost) || decimal.TryParse(tb_Discount.Text, out Disount))
            {

            }
            else
            {
                MessageBox.Show("Please put the correct number;");
                return;
            }

            Date = dateTime_ReciteDate.Value.Date;


            if (Date > DateTime.Today)
            {
                MessageBox.Show("The selected date cannot be in the future.");
                return;
            }

            if (ReciteType.SelectedValue is byte type)
            {
                Type = type;
            }


            DialogResult = DialogResult.OK;


        }

        private void Tb_TransportCost_TextChanged(object sender, EventArgs e)
        {

        }
        private void ConfigureComboBox(AnbarDataSet.WarehousesDataTable warehouses, AnbarDataSet.PartiesDataTable parties)
        {
            cmbwarehouse.DisplayMember = nameof(AnbarDataSet.WarehousesRow.Name);
            cmbwarehouse.ValueMember = nameof(AnbarDataSet.WarehousesRow.Id);
            cmbwarehouse.DataSource = warehouses;

            cmbParty.DisplayMember = nameof(AnbarDataSet.PartiesRow.Name);
            cmbParty.ValueMember = nameof(AnbarDataSet.PartiesRow.Id);
            cmbParty.DataSource = parties;

            var receiptTypes = Enum.GetValues(typeof(ReciteType)).Cast<ReciteType>().Select
               (u => new { Name = u.ToString(),Value=(byte)u}).ToList();

            ReciteType.DisplayMember = "Name";
            ReciteType.ValueMember = "Value";
            ReciteType.DataSource = receiptTypes;


        }

        private void ConfigureDate()
        {
            dateTime_ReciteDate.Value = DateTime.Today;

        }

        private void AddReceiptForm_Load(object sender, EventArgs e)
        {

        }
    }
}
