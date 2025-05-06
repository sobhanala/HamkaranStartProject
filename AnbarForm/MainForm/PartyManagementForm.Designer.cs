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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_city = new System.Windows.Forms.TextBox();
            this.txt_postalCode = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tb_street = new System.Windows.Forms.TextBox();
            this.tb_country = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(26, 25);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(100, 20);
            this.txt_name.TabIndex = 1;
            this.txt_name.Text = "Name";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txt_email
            // 
            this.txt_email.Location = new System.Drawing.Point(26, 51);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(100, 20);
            this.txt_email.TabIndex = 3;
            this.txt_email.Text = "Email";
            // 
            // txt_city
            // 
            this.txt_city.Location = new System.Drawing.Point(26, 103);
            this.txt_city.Name = "txt_city";
            this.txt_city.Size = new System.Drawing.Size(100, 20);
            this.txt_city.TabIndex = 4;
            this.txt_city.Text = "City";
            this.txt_city.TextChanged += new System.EventHandler(this.Txt_city_TextChanged);
            // 
            // txt_postalCode
            // 
            this.txt_postalCode.Location = new System.Drawing.Point(26, 155);
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
            this.comboBox1.Location = new System.Drawing.Point(26, 181);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Text = "Type";
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(26, 222);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(121, 68);
            this.btn_submit.TabIndex = 7;
            this.btn_submit.Text = "Save";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.Btn_submit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(179, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 303);
            this.dataGridView1.TabIndex = 8;
            // 
            // tb_street
            // 
            this.tb_street.Location = new System.Drawing.Point(26, 129);
            this.tb_street.Name = "tb_street";
            this.tb_street.Size = new System.Drawing.Size(100, 20);
            this.tb_street.TabIndex = 9;
            this.tb_street.Text = "Street";
            // 
            // tb_country
            // 
            this.tb_country.Location = new System.Drawing.Point(26, 77);
            this.tb_country.Name = "tb_country";
            this.tb_country.Size = new System.Drawing.Size(100, 20);
            this.tb_country.TabIndex = 10;
            this.tb_country.Text = "Country";
            // 
            // PartyManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 374);
            this.Controls.Add(this.tb_country);
            this.Controls.Add(this.tb_street);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txt_postalCode);
            this.Controls.Add(this.txt_city);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.label1);
            this.Name = "PartyManagementForm";
            this.Text = "PartyManagementForm";
            this.Load += new System.EventHandler(this.PartyManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
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
    }
}