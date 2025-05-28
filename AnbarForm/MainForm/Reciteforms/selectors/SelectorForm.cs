using System;
using System.Data;
using System.Windows.Forms;

namespace AnbarForm.MainForm.Reciteforms.selectors
{
    public class SelectorForm<T> : BaseSelector where T : DataTable
    {
        private BindingSource _bindingSource = new BindingSource();
        public DataRow SelectedRow { get; private set; }
        private readonly string _filterColumn;

        public SelectorForm(T table, string filterColumn, string displayText)
        {

            _filterColumn = filterColumn;

            if (!table.Columns.Contains(_filterColumn))
                throw new ArgumentException($"Column '{_filterColumn}' does not exist in table '{typeof(T).Name}'.");


            InitializeComponent();

            base.FieldName = displayText;
            base.SearchField = filterColumn;
            _bindingSource.DataSource = table;

            dataGridView1.DataSource = _bindingSource;

            searchbox.TextChanged += SearchBox_TextChanged;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            UpdateFieldDisplay();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchbox.Text.Replace("'", "''");
            _bindingSource.Filter = $"{_filterColumn} LIKE '%{filterText}%'";
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingSource.Current is DataRowView rowView)
            {
                SelectedRow = rowView.Row;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(600, 197);
            this.Name = "SelectorForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}