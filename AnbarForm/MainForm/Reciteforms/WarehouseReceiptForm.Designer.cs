﻿namespace AnbarForm.MainForm.Reciteforms
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
            this.DeleteReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewMaster = new System.Windows.Forms.DataGridView();
            this.ReceiptNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarehouseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddRecite = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteReceipt,
            this.ViewReceipt});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(150, 48);
            // 
            // DeleteReceipt
            // 
            this.DeleteReceipt.Name = "DeleteReceipt";
            this.DeleteReceipt.Size = new System.Drawing.Size(180, 22);
            this.DeleteReceipt.Text = "Delete Receipt";
            this.DeleteReceipt.Click += new System.EventHandler(this.DeleteReceipt_Click);
            // 
            // ViewReceipt
            // 
            this.ViewReceipt.Name = "ViewReceipt";
            this.ViewReceipt.Size = new System.Drawing.Size(180, 22);
            this.ViewReceipt.Text = "View Receipt";
            this.ViewReceipt.Click += new System.EventHandler(this.ViewReceipt_Click);
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
            this.dataGridViewMaster.ContextMenuStrip = this.contextMenuStrip1;
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
            // WarehouseReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 482);
            this.Controls.Add(this.AddRecite);
            this.Controls.Add(this.dataGridViewMaster);
            this.Name = "WarehouseReceiptForm";
            this.Text = "WarehouseReceiptForm ";
            this.Load += new System.EventHandler(this.WarehouseReceiptForm_Load_1);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DeleteReceipt;
        private System.Windows.Forms.ToolStripMenuItem ViewReceipt;
        private System.Windows.Forms.Button AddRecite;
        private System.Windows.Forms.DataGridView dataGridViewMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarehouseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptDate;
    }
}