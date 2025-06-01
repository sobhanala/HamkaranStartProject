namespace AnbarForm.MainForm.Reciteforms
{
    partial class ReciteAnbarFormincome
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.nestedTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.numDiscount = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCost = new System.Windows.Forms.Label();
            this.numCost = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDate = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblParty = new System.Windows.Forms.Label();
            this.BtnSelectParty = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtWarehouse = new System.Windows.Forms.Label();
            this.BtnSelectWarehouse = new System.Windows.Forms.Button();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnAddRow = new System.Windows.Forms.Button();
            this.BtnDeleteRow = new System.Windows.Forms.Button();
            this.dgReciteItem = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.grpHeader.SuspendLayout();
            this.nestedTableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            this.flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCost)).BeginInit();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReciteItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_Save, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.grpHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel7, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1443, 627);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.TableLayoutPanel1_Paint);
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Location = new System.Drawing.Point(3, 567);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(1437, 57);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.nestedTableLayoutPanel);
            this.grpHeader.Location = new System.Drawing.Point(3, 3);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1437, 182);
            this.grpHeader.TabIndex = 0;
            this.grpHeader.TabStop = false;
            this.grpHeader.Text = "Receipt Information";
            // 
            // nestedTableLayoutPanel
            // 
            this.nestedTableLayoutPanel.ColumnCount = 3;
            this.nestedTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.nestedTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.nestedTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.nestedTableLayoutPanel.Controls.Add(this.flowLayoutPanel6, 2, 1);
            this.nestedTableLayoutPanel.Controls.Add(this.flowLayoutPanel5, 1, 1);
            this.nestedTableLayoutPanel.Controls.Add(this.flowLayoutPanel4, 0, 1);
            this.nestedTableLayoutPanel.Controls.Add(this.flowLayoutPanel3, 2, 0);
            this.nestedTableLayoutPanel.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.nestedTableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.nestedTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nestedTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.nestedTableLayoutPanel.Name = "nestedTableLayoutPanel";
            this.nestedTableLayoutPanel.RowCount = 2;
            this.nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.nestedTableLayoutPanel.Size = new System.Drawing.Size(1431, 163);
            this.nestedTableLayoutPanel.TabIndex = 1;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.lblDiscount);
            this.flowLayoutPanel6.Controls.Add(this.numDiscount);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(957, 84);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(471, 76);
            this.flowLayoutPanel6.TabIndex = 5;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblDiscount.Location = new System.Drawing.Point(3, 0);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(67, 26);
            this.lblDiscount.TabIndex = 0;
            this.lblDiscount.Text = "Discount:";
            // 
            // numDiscount
            // 
            this.numDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numDiscount.Location = new System.Drawing.Point(76, 3);
            this.numDiscount.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new System.Drawing.Size(120, 20);
            this.numDiscount.TabIndex = 1;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.lblCost);
            this.flowLayoutPanel5.Controls.Add(this.numCost);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(480, 84);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(471, 76);
            this.flowLayoutPanel5.TabIndex = 4;
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblCost.Location = new System.Drawing.Point(3, 0);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(40, 26);
            this.lblCost.TabIndex = 0;
            this.lblCost.Text = "Cost:";
            // 
            // numCost
            // 
            this.numCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numCost.Location = new System.Drawing.Point(49, 3);
            this.numCost.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numCost.Name = "numCost";
            this.numCost.Size = new System.Drawing.Size(120, 20);
            this.numCost.TabIndex = 1;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.lblType);
            this.flowLayoutPanel4.Controls.Add(this.cmbType);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 84);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(471, 76);
            this.flowLayoutPanel4.TabIndex = 3;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblType.Location = new System.Drawing.Point(3, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(44, 27);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Type:";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(53, 3);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 21);
            this.cmbType.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.lblDate);
            this.flowLayoutPanel3.Controls.Add(this.dateTimePicker1);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(957, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(471, 75);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblDate.Location = new System.Drawing.Point(3, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(42, 26);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(51, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(361, 20);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.Value = new System.DateTime(2025, 5, 28, 10, 29, 17, 0);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lblParty);
            this.flowLayoutPanel2.Controls.Add(this.BtnSelectParty);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(480, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(471, 75);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblParty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.lblParty.Location = new System.Drawing.Point(3, 0);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(52, 29);
            this.lblParty.TabIndex = 0;
            this.lblParty.Text = "Partys:";
            // 
            // BtnSelectParty
            // 
            this.BtnSelectParty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelectParty.Location = new System.Drawing.Point(61, 3);
            this.BtnSelectParty.Name = "BtnSelectParty";
            this.BtnSelectParty.Size = new System.Drawing.Size(75, 23);
            this.BtnSelectParty.TabIndex = 1;
            this.BtnSelectParty.Text = "...";
            this.BtnSelectParty.UseVisualStyleBackColor = true;
            this.BtnSelectParty.Click += new System.EventHandler(this.BtnSelectParty_Click_1);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.txtWarehouse);
            this.flowLayoutPanel1.Controls.Add(this.BtnSelectWarehouse);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(471, 75);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // txtWarehouse
            // 
            this.txtWarehouse.AutoSize = true;
            this.txtWarehouse.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtWarehouse.Location = new System.Drawing.Point(3, 0);
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.Size = new System.Drawing.Size(85, 29);
            this.txtWarehouse.TabIndex = 0;
            this.txtWarehouse.Text = "Warehouse:";
            // 
            // BtnSelectWarehouse
            // 
            this.BtnSelectWarehouse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelectWarehouse.Location = new System.Drawing.Point(94, 3);
            this.BtnSelectWarehouse.Name = "BtnSelectWarehouse";
            this.BtnSelectWarehouse.Size = new System.Drawing.Size(75, 23);
            this.BtnSelectWarehouse.TabIndex = 1;
            this.BtnSelectWarehouse.Text = "...";
            this.BtnSelectWarehouse.UseVisualStyleBackColor = true;
            this.BtnSelectWarehouse.Click += new System.EventHandler(this.BtnSelectWarehouse_Click_1);
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Controls.Add(this.BtnAddRow);
            this.flowLayoutPanel7.Controls.Add(this.BtnDeleteRow);
            this.flowLayoutPanel7.Controls.Add(this.dgReciteItem);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(3, 191);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(1437, 370);
            this.flowLayoutPanel7.TabIndex = 4;
            // 
            // BtnAddRow
            // 
            this.BtnAddRow.BackColor = System.Drawing.Color.LimeGreen;
            this.BtnAddRow.Location = new System.Drawing.Point(3, 3);
            this.BtnAddRow.Name = "BtnAddRow";
            this.BtnAddRow.Size = new System.Drawing.Size(28, 23);
            this.BtnAddRow.TabIndex = 2;
            this.BtnAddRow.UseVisualStyleBackColor = false;
            this.BtnAddRow.Click += new System.EventHandler(this.BtnAddRow_Click);
            // 
            // BtnDeleteRow
            // 
            this.BtnDeleteRow.BackColor = System.Drawing.Color.OrangeRed;
            this.BtnDeleteRow.Location = new System.Drawing.Point(37, 3);
            this.BtnDeleteRow.Name = "BtnDeleteRow";
            this.BtnDeleteRow.Size = new System.Drawing.Size(30, 23);
            this.BtnDeleteRow.TabIndex = 1;
            this.BtnDeleteRow.UseVisualStyleBackColor = false;
            this.BtnDeleteRow.Click += new System.EventHandler(this.BtnDeleteRow_Click);
            // 
            // dgReciteItem
            // 
            this.dgReciteItem.AllowUserToAddRows = false;
            this.dgReciteItem.AllowUserToDeleteRows = false;
            this.dgReciteItem.AllowUserToOrderColumns = true;
            this.dgReciteItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReciteItem.Location = new System.Drawing.Point(3, 32);
            this.dgReciteItem.Name = "dgReciteItem";
            this.dgReciteItem.Size = new System.Drawing.Size(1434, 356);
            this.dgReciteItem.TabIndex = 3;
            this.dgReciteItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgReciteItem_CellContentClick_1);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ReciteAnbarFormincome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 627);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ReciteAnbarFormincome";
            this.Text = "ReciteAnbarFormincome";
            this.Load += new System.EventHandler(this.ReciteAnbarForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpHeader.ResumeLayout(false);
            this.nestedTableLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCost)).EndInit();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgReciteItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.TableLayoutPanel nestedTableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.NumericUpDown numDiscount;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.NumericUpDown numCost;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lblParty;
        private System.Windows.Forms.Button BtnSelectParty;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label txtWarehouse;
        private System.Windows.Forms.Button BtnSelectWarehouse;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.DataGridView dgReciteItem;
        private System.Windows.Forms.Button BtnDeleteRow;
        private System.Windows.Forms.Button BtnAddRow;
    }
}