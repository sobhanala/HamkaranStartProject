﻿using System;
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
using AnbarDomain.Warehouse;

namespace AnbarForm.MainForm.Reciteforms
{
    public partial class WarehouseSelectorForm : Form
    {
        private AnbarDataSet _anbar; 

        private WarehousesTableAdapter _warehouseAdapter = new WarehousesTableAdapter();
        public AnbarDataSet.WarehousesRow SelectedWarehouse { get; private set; }

        private BindingSource _bindingSource;
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
            _anbar = new AnbarDataSet();
            _warehouseAdapter.Fill(_anbar.Warehouses);
            _bindingSource.DataSource = _anbar.Warehouses;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingSource.Current is DataRowView rowView )
            {
                SelectedWarehouse = (AnbarDataSet.WarehousesRow)rowView.Row;
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
