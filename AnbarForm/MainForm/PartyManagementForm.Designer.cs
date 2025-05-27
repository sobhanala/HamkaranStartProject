using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;

namespace AnbarForm.MainForm
{
    partial class PartyManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_city = new System.Windows.Forms.TextBox();
            this.txt_postalCode = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tb_street = new System.Windows.Forms.TextBox();
            this.tb_country = new System.Windows.Forms.TextBox();
            this.btn_save_all = new System.Windows.Forms.Button();
            this.anbarDataSet1 = new AnbarDomain.Tabels.AnbarDataSet();
            this.partiesTableAdapter1 = new AnbarDomain.Tabels.AnbarDataSetTableAdapters.PartiesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anbarDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(10, 23);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(100, 20);
            this.txt_name.TabIndex = 1;
            this.txt_name.Text = "Name";
            this.txt_name.TextChanged += new System.EventHandler(this.Txt_name_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txt_email
            // 
            this.txt_email.Location = new System.Drawing.Point(10, 58);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(100, 20);
            this.txt_email.TabIndex = 3;
            this.txt_email.Text = "Email";
            // 
            // txt_city
            // 
            this.txt_city.Location = new System.Drawing.Point(10, 137);
            this.txt_city.Name = "txt_city";
            this.txt_city.Size = new System.Drawing.Size(100, 20);
            this.txt_city.TabIndex = 4;
            this.txt_city.Text = "City";
            this.txt_city.TextChanged += new System.EventHandler(this.Txt_city_TextChanged);
            // 
            // txt_postalCode
            // 
            this.txt_postalCode.Location = new System.Drawing.Point(10, 216);
            this.txt_postalCode.Name = "txt_postalCode";
            this.txt_postalCode.Size = new System.Drawing.Size(100, 20);
            this.txt_postalCode.TabIndex = 5;
            this.txt_postalCode.Text = "PostalCode";
            this.txt_postalCode.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Vendor",
            "customer",
            "supplier"});
            this.comboBox1.Location = new System.Drawing.Point(10, 253);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Text = "Type";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(10, 294);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(121, 68);
            this.btn_submit.TabIndex = 7;
            this.btn_submit.Text = "Save";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.Btn_submit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.delete});
            this.dataGridView1.Location = new System.Drawing.Point(139, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(280, 278);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // delete
            // 
            this.delete.HeaderText = "Delete";
            this.delete.Name = "delete";
            this.delete.Text = "Delete";
            this.delete.UseColumnTextForButtonValue = true;
            // 
            // tb_street
            // 
            this.tb_street.Location = new System.Drawing.Point(10, 176);
            this.tb_street.Name = "tb_street";
            this.tb_street.Size = new System.Drawing.Size(100, 20);
            this.tb_street.TabIndex = 9;
            this.tb_street.Text = "Street";
            // 
            // tb_country
            // 
            this.tb_country.Location = new System.Drawing.Point(10, 100);
            this.tb_country.Name = "tb_country";
            this.tb_country.Size = new System.Drawing.Size(100, 20);
            this.tb_country.TabIndex = 10;
            this.tb_country.Text = "Country";
            this.tb_country.TextChanged += new System.EventHandler(this.Tb_country_TextChanged);
            // 
            // btn_save_all
            // 
            this.btn_save_all.Location = new System.Drawing.Point(139, 294);
            this.btn_save_all.Name = "btn_save_all";
            this.btn_save_all.Size = new System.Drawing.Size(280, 68);
            this.btn_save_all.TabIndex = 11;
            this.btn_save_all.Text = "Save All";
            this.btn_save_all.UseVisualStyleBackColor = true;
            this.btn_save_all.Click += new System.EventHandler(this.Btn_save_all_Click);
            // 
            // anbarDataSet1
            // 
            this.anbarDataSet1.DataSetName = "AnbarDataSet";
            this.anbarDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // partiesTableAdapter1
            // 
            this.partiesTableAdapter1.ClearBeforeFill = true;
            // 
            // PartyManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 407);
            this.Controls.Add(this.btn_save_all);
            this.Controls.Add(this.tb_country);
            this.Controls.Add(this.tb_street);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txt_postalCode);
            this.Controls.Add(this.txt_city);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.txt_name);
            this.Name = "PartyManagementForm";
            this.Text = "PartyManagementForm";
            this.Load += new System.EventHandler(this.PartyManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anbarDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_city;
        private System.Windows.Forms.TextBox txt_postalCode;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox tb_street;
        private System.Windows.Forms.TextBox tb_country;
        private System.Windows.Forms.Button btn_save_all;
        private AnbarDataSet anbarDataSet1;
        private PartiesTableAdapter partiesTableAdapter1;
        private System.Windows.Forms.DataGridViewButtonColumn delete;
    }
}