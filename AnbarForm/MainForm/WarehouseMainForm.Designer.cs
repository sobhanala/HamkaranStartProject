namespace AnbarForm.MainForm
{
    partial class UserForm
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_save_all = new System.Windows.Forms.Button();
            this.tb_country = new System.Windows.Forms.TextBox();
            this.tb_street = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_submit = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txt_postalCode = new System.Windows.Forms.TextBox();
            this.txt_city = new System.Windows.Forms.TextBox();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripTextBox1,
            this.toolStripSeparator1,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(182, 84);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_save_all);
            this.splitContainer1.Panel2.Controls.Add(this.tb_country);
            this.splitContainer1.Panel2.Controls.Add(this.tb_street);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.btn_submit);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel2.Controls.Add(this.txt_postalCode);
            this.splitContainer1.Panel2.Controls.Add(this.txt_city);
            this.splitContainer1.Panel2.Controls.Add(this.txt_email);
            this.splitContainer1.Panel2.Controls.Add(this.txt_name);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(938, 429);
            this.splitContainer1.SplitterDistance = 312;
            this.splitContainer1.TabIndex = 1;
            // 
            // btn_save_all
            // 
            this.btn_save_all.Location = new System.Drawing.Point(200, 319);
            this.btn_save_all.Name = "btn_save_all";
            this.btn_save_all.Size = new System.Drawing.Size(280, 68);
            this.btn_save_all.TabIndex = 21;
            this.btn_save_all.Text = "Save All";
            this.btn_save_all.UseVisualStyleBackColor = true;
            // 
            // tb_country
            // 
            this.tb_country.Location = new System.Drawing.Point(71, 125);
            this.tb_country.Name = "tb_country";
            this.tb_country.Size = new System.Drawing.Size(100, 20);
            this.tb_country.TabIndex = 20;
            this.tb_country.Text = "Country";
            // 
            // tb_street
            // 
            this.tb_street.Location = new System.Drawing.Point(71, 201);
            this.tb_street.Name = "tb_street";
            this.tb_street.Size = new System.Drawing.Size(100, 20);
            this.tb_street.TabIndex = 19;
            this.tb_street.Text = "Street";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.delete});
            this.dataGridView1.Location = new System.Drawing.Point(200, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(280, 278);
            this.dataGridView1.TabIndex = 18;
            // 
            // delete
            // 
            this.delete.HeaderText = "Delete";
            this.delete.Name = "delete";
            this.delete.Text = "Delete";
            this.delete.UseColumnTextForButtonValue = true;
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(71, 319);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(121, 68);
            this.btn_submit.TabIndex = 17;
            this.btn_submit.Text = "Save";
            this.btn_submit.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Vendor",
            "customer",
            "supplier"});
            this.comboBox1.Location = new System.Drawing.Point(71, 278);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.Text = "Type";
            // 
            // txt_postalCode
            // 
            this.txt_postalCode.Location = new System.Drawing.Point(71, 241);
            this.txt_postalCode.Name = "txt_postalCode";
            this.txt_postalCode.Size = new System.Drawing.Size(100, 20);
            this.txt_postalCode.TabIndex = 15;
            this.txt_postalCode.Text = "PostalCode";
            // 
            // txt_city
            // 
            this.txt_city.Location = new System.Drawing.Point(71, 162);
            this.txt_city.Name = "txt_city";
            this.txt_city.Size = new System.Drawing.Size(100, 20);
            this.txt_city.TabIndex = 14;
            this.txt_city.Text = "City";
            // 
            // txt_email
            // 
            this.txt_email.Location = new System.Drawing.Point(71, 83);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(100, 20);
            this.txt_email.TabIndex = 13;
            this.txt_email.Text = "Email";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(71, 48);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(100, 20);
            this.txt_name.TabIndex = 12;
            this.txt_name.Text = "Name";
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(938, 429);
            this.Controls.Add(this.splitContainer1);
            this.Name = "UserForm";
            this.Text = "AnbarForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_save_all;
        private System.Windows.Forms.TextBox tb_country;
        private System.Windows.Forms.TextBox tb_street;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewButtonColumn delete;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txt_postalCode;
        private System.Windows.Forms.TextBox txt_city;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_name;
    }
}