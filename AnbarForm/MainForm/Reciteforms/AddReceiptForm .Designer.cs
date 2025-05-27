namespace AnbarForm.MainForm
{
    partial class AddReceiptForm
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
            this.cmbParty = new System.Windows.Forms.ComboBox();
            this.cmbwarehouse = new System.Windows.Forms.ComboBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.tb_TransportCost = new System.Windows.Forms.TextBox();
            this.tb_Discount = new System.Windows.Forms.TextBox();
            this.ReciteType = new System.Windows.Forms.ComboBox();
            this.dateTime_ReciteDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // cmbParty
            // 
            this.cmbParty.FormattingEnabled = true;
            this.cmbParty.Location = new System.Drawing.Point(266, 35);
            this.cmbParty.Name = "cmbParty";
            this.cmbParty.Size = new System.Drawing.Size(121, 21);
            this.cmbParty.TabIndex = 0;
            this.cmbParty.Text = "party";
            // 
            // cmbwarehouse
            // 
            this.cmbwarehouse.FormattingEnabled = true;
            this.cmbwarehouse.Location = new System.Drawing.Point(139, 35);
            this.cmbwarehouse.Name = "cmbwarehouse";
            this.cmbwarehouse.Size = new System.Drawing.Size(121, 21);
            this.cmbwarehouse.TabIndex = 1;
            this.cmbwarehouse.Text = "Warehouse";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(812, 11);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(142, 65);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.Btn_ok_Click);
            // 
            // tb_TransportCost
            // 
            this.tb_TransportCost.Location = new System.Drawing.Point(393, 34);
            this.tb_TransportCost.Multiline = true;
            this.tb_TransportCost.Name = "tb_TransportCost";
            this.tb_TransportCost.Size = new System.Drawing.Size(87, 21);
            this.tb_TransportCost.TabIndex = 3;
            this.tb_TransportCost.Text = "TransportCost";
            this.tb_TransportCost.TextChanged += new System.EventHandler(this.Tb_TransportCost_TextChanged);
            // 
            // tb_Discount
            // 
            this.tb_Discount.Location = new System.Drawing.Point(486, 36);
            this.tb_Discount.Name = "tb_Discount";
            this.tb_Discount.Size = new System.Drawing.Size(81, 20);
            this.tb_Discount.TabIndex = 4;
            this.tb_Discount.Text = "Discount";
            // 
            // ReciteType
            // 
            this.ReciteType.FormattingEnabled = true;
            this.ReciteType.Items.AddRange(new object[] {
            "0",
            "1"});
            this.ReciteType.Location = new System.Drawing.Point(12, 34);
            this.ReciteType.Name = "ReciteType";
            this.ReciteType.Size = new System.Drawing.Size(121, 21);
            this.ReciteType.TabIndex = 5;
            this.ReciteType.Text = "ReciteType ";
            // 
            // dateTime_ReciteDate
            // 
            this.dateTime_ReciteDate.Location = new System.Drawing.Point(573, 36);
            this.dateTime_ReciteDate.Name = "dateTime_ReciteDate";
            this.dateTime_ReciteDate.Size = new System.Drawing.Size(200, 20);
            this.dateTime_ReciteDate.TabIndex = 6;
            // 
            // AddReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 82);
            this.Controls.Add(this.dateTime_ReciteDate);
            this.Controls.Add(this.ReciteType);
            this.Controls.Add(this.tb_Discount);
            this.Controls.Add(this.tb_TransportCost);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.cmbwarehouse);
            this.Controls.Add(this.cmbParty);
            this.Name = "AddReceiptForm";
            this.Text = "AddReceiptForm ";
            this.Load += new System.EventHandler(this.AddReceiptForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbParty;
        private System.Windows.Forms.ComboBox cmbwarehouse;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.TextBox tb_TransportCost;
        private System.Windows.Forms.TextBox tb_Discount;
        private System.Windows.Forms.ComboBox ReciteType;
        private System.Windows.Forms.DateTimePicker dateTime_ReciteDate;
    }
}