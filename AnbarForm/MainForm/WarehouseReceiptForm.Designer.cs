namespace AnbarForm.MainForm
{
    partial class WarehouseReceiptForm
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.EditReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewMaster = new System.Windows.Forms.DataGridView();
            this.ReceiptNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarehouseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddRecite = new System.Windows.Forms.Button();
            this.panel_Details = new System.Windows.Forms.Panel();
            this.labelPartyName = new System.Windows.Forms.Label();
            this.lblWareHouseName = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.lblTransportCost = new System.Windows.Forms.Label();
            this.lblCreatedAt = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblUpdateBy = new System.Windows.Forms.Label();
            this.lblUpdatedAt = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblReciteDate = new System.Windows.Forms.Label();
            this.btn_PanelClose = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaster)).BeginInit();
            this.panel_Details.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.DeleteReceipt,
            this.EditReceipt});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(150, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem1.Text = "View Details";
            // 
            // DeleteReceipt
            // 
            this.DeleteReceipt.Name = "DeleteReceipt";
            this.DeleteReceipt.Size = new System.Drawing.Size(149, 22);
            this.DeleteReceipt.Text = "Delete Receipt";
            // 
            // EditReceipt
            // 
            this.EditReceipt.Name = "EditReceipt";
            this.EditReceipt.Size = new System.Drawing.Size(149, 22);
            this.EditReceipt.Text = "Edit Receipt";
            // 
            // dataGridViewMaster
            // 
            this.dataGridViewMaster.AllowUserToAddRows = false;
            this.dataGridViewMaster.AllowUserToDeleteRows = false;
            this.dataGridViewMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReceiptNumber,
            this.PartyId,
            this.WarehouseId,
            this.ReceiptDate});
            this.dataGridViewMaster.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewMaster.Name = "dataGridViewMaster";
            this.dataGridViewMaster.ReadOnly = true;
            this.dataGridViewMaster.RowHeadersWidth = 51;
            this.dataGridViewMaster.Size = new System.Drawing.Size(554, 480);
            this.dataGridViewMaster.TabIndex = 1;
            // 
            // ReceiptNumber
            // 
            this.ReceiptNumber.DataPropertyName = "ReceiptNumber";
            this.ReceiptNumber.HeaderText = "ReceiptNumber";
            this.ReceiptNumber.Name = "ReceiptNumber";
            this.ReceiptNumber.ReadOnly = true;
            // 
            // PartyId
            // 
            this.PartyId.DataPropertyName = "PartyId";
            this.PartyId.HeaderText = "PartyId";
            this.PartyId.Name = "PartyId";
            this.PartyId.ReadOnly = true;
            // 
            // WarehouseId
            // 
            this.WarehouseId.DataPropertyName = "WarehouseId";
            this.WarehouseId.HeaderText = "WarehouseId";
            this.WarehouseId.Name = "WarehouseId";
            this.WarehouseId.ReadOnly = true;
            // 
            // ReceiptDate
            // 
            this.ReceiptDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReceiptDate.DataPropertyName = "ReceiptDate";
            this.ReceiptDate.HeaderText = "ReceiptDate";
            this.ReceiptDate.Name = "ReceiptDate";
            this.ReceiptDate.ReadOnly = true;
            // 
            // AddRecite
            // 
            this.AddRecite.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.AddRecite.Location = new System.Drawing.Point(559, 0);
            this.AddRecite.Margin = new System.Windows.Forms.Padding(2);
            this.AddRecite.Name = "AddRecite";
            this.AddRecite.Size = new System.Drawing.Size(98, 480);
            this.AddRecite.TabIndex = 2;
            this.AddRecite.Text = "AddRecite";
            this.AddRecite.UseVisualStyleBackColor = false;
            this.AddRecite.Click += new System.EventHandler(this.AddRecite_Click);
            // 
            // panel_Details
            // 
            this.panel_Details.Controls.Add(this.btn_PanelClose);
            this.panel_Details.Controls.Add(this.lblReciteDate);
            this.panel_Details.Controls.Add(this.lblType);
            this.panel_Details.Controls.Add(this.lblUpdatedAt);
            this.panel_Details.Controls.Add(this.lblUpdateBy);
            this.panel_Details.Controls.Add(this.lblCreatedBy);
            this.panel_Details.Controls.Add(this.lblCreatedAt);
            this.panel_Details.Controls.Add(this.lblTransportCost);
            this.panel_Details.Controls.Add(this.lblTotalCost);
            this.panel_Details.Controls.Add(this.lblDiscount);
            this.panel_Details.Controls.Add(this.lblWareHouseName);
            this.panel_Details.Controls.Add(this.labelPartyName);
            this.panel_Details.Location = new System.Drawing.Point(662, 0);
            this.panel_Details.Name = "panel_Details";
            this.panel_Details.Size = new System.Drawing.Size(182, 471);
            this.panel_Details.TabIndex = 3;
            // 
            // labelPartyName
            // 
            this.labelPartyName.AutoSize = true;
            this.labelPartyName.Location = new System.Drawing.Point(3, 6);
            this.labelPartyName.Name = "labelPartyName";
            this.labelPartyName.Size = new System.Drawing.Size(59, 13);
            this.labelPartyName.TabIndex = 0;
            this.labelPartyName.Text = "PartyName";
            // 
            // lblWareHouseName
            // 
            this.lblWareHouseName.AutoSize = true;
            this.lblWareHouseName.Location = new System.Drawing.Point(3, 47);
            this.lblWareHouseName.Name = "lblWareHouseName";
            this.lblWareHouseName.Size = new System.Drawing.Size(92, 13);
            this.lblWareHouseName.TabIndex = 1;
            this.lblWareHouseName.Text = "WareHouseName";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(3, 88);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(49, 13);
            this.lblDiscount.TabIndex = 2;
            this.lblDiscount.Text = "Discount";
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Location = new System.Drawing.Point(3, 129);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(52, 13);
            this.lblTotalCost.TabIndex = 3;
            this.lblTotalCost.Text = "TotalCost";
            this.lblTotalCost.Click += new System.EventHandler(this.Label3_Click);
            // 
            // lblTransportCost
            // 
            this.lblTransportCost.AutoSize = true;
            this.lblTransportCost.Location = new System.Drawing.Point(3, 170);
            this.lblTransportCost.Name = "lblTransportCost";
            this.lblTransportCost.Size = new System.Drawing.Size(73, 13);
            this.lblTransportCost.TabIndex = 4;
            this.lblTransportCost.Text = "TransportCost";
            // 
            // lblCreatedAt
            // 
            this.lblCreatedAt.AutoSize = true;
            this.lblCreatedAt.Location = new System.Drawing.Point(3, 293);
            this.lblCreatedAt.Name = "lblCreatedAt";
            this.lblCreatedAt.Size = new System.Drawing.Size(54, 13);
            this.lblCreatedAt.TabIndex = 5;
            this.lblCreatedAt.Text = "CreatedAt";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(6, 334);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(56, 13);
            this.lblCreatedBy.TabIndex = 6;
            this.lblCreatedBy.Text = "CreatedBy";
            // 
            // lblUpdateBy
            // 
            this.lblUpdateBy.AutoSize = true;
            this.lblUpdateBy.Location = new System.Drawing.Point(6, 375);
            this.lblUpdateBy.Name = "lblUpdateBy";
            this.lblUpdateBy.Size = new System.Drawing.Size(54, 13);
            this.lblUpdateBy.TabIndex = 7;
            this.lblUpdateBy.Text = "UpdateBy";
            // 
            // lblUpdatedAt
            // 
            this.lblUpdatedAt.AutoSize = true;
            this.lblUpdatedAt.Location = new System.Drawing.Point(6, 416);
            this.lblUpdatedAt.Name = "lblUpdatedAt";
            this.lblUpdatedAt.Size = new System.Drawing.Size(58, 13);
            this.lblUpdatedAt.TabIndex = 8;
            this.lblUpdatedAt.Text = "UpdatedAt";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(3, 211);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 9;
            this.lblType.Text = "Type";
            // 
            // lblReciteDate
            // 
            this.lblReciteDate.AutoSize = true;
            this.lblReciteDate.Location = new System.Drawing.Point(6, 252);
            this.lblReciteDate.Name = "lblReciteDate";
            this.lblReciteDate.Size = new System.Drawing.Size(61, 13);
            this.lblReciteDate.TabIndex = 10;
            this.lblReciteDate.Text = "ReciteDate";
            // 
            // btn_PanelClose
            // 
            this.btn_PanelClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PanelClose.Location = new System.Drawing.Point(3, 432);
            this.btn_PanelClose.Name = "btn_PanelClose";
            this.btn_PanelClose.Size = new System.Drawing.Size(89, 39);
            this.btn_PanelClose.TabIndex = 11;
            this.btn_PanelClose.Text = "Close";
            this.btn_PanelClose.UseVisualStyleBackColor = true;
            this.btn_PanelClose.Click += new System.EventHandler(this.Btn_PanelClose_Click);
            // 
            // WarehouseReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 472);
            this.Controls.Add(this.panel_Details);
            this.Controls.Add(this.AddRecite);
            this.Controls.Add(this.dataGridViewMaster);
            this.Name = "WarehouseReceiptForm";
            this.Text = "WarehouseReceiptForm ";
            this.Load += new System.EventHandler(this.WarehouseReceiptForm_Load_1);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaster)).EndInit();
            this.panel_Details.ResumeLayout(false);
            this.panel_Details.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DeleteReceipt;
        private System.Windows.Forms.ToolStripMenuItem EditReceipt;
        private System.Windows.Forms.Button AddRecite;
        private System.Windows.Forms.DataGridView dataGridViewMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarehouseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptDate;
        private System.Windows.Forms.Panel panel_Details;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblWareHouseName;
        private System.Windows.Forms.Label labelPartyName;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblCreatedAt;
        private System.Windows.Forms.Label lblTransportCost;
        private System.Windows.Forms.Label lblUpdatedAt;
        private System.Windows.Forms.Label lblUpdateBy;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblReciteDate;
        private System.Windows.Forms.Button btn_PanelClose;
    }
}