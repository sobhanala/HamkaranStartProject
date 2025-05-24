namespace AnbarForm.MainForm
{
    partial class EditReciteForm
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
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.warehouseReceiptItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Gb_AddReciteItem = new System.Windows.Forms.GroupBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numeric_UnitPrice = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numeric_Quantity = new System.Windows.Forms.NumericUpDown();
            this.cb_Products = new System.Windows.Forms.ComboBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.HeaderRecite = new System.Windows.Forms.GroupBox();
            this.lblReceiptDate = new System.Windows.Forms.Label();
            this.lblReceiptType = new System.Windows.Forms.Label();
            this.lblReceiptNumber = new System.Windows.Forms.Label();
            this.btn_Updateitem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warehouseReceiptItemsBindingSource)).BeginInit();
            this.Gb_AddReciteItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_UnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Quantity)).BeginInit();
            this.HeaderRecite.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Location = new System.Drawing.Point(260, 12);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.Size = new System.Drawing.Size(411, 465);
            this.dataGridViewItems.TabIndex = 0;
            // 
            // warehouseReceiptItemsBindingSource
            // 
            this.warehouseReceiptItemsBindingSource.DataMember = "WarehouseReceiptItems";
            // 
            // Gb_AddReciteItem
            // 
            this.Gb_AddReciteItem.Controls.Add(this.btnAddItem);
            this.Gb_AddReciteItem.Controls.Add(this.label3);
            this.Gb_AddReciteItem.Controls.Add(this.numeric_UnitPrice);
            this.Gb_AddReciteItem.Controls.Add(this.label2);
            this.Gb_AddReciteItem.Controls.Add(this.label1);
            this.Gb_AddReciteItem.Controls.Add(this.numeric_Quantity);
            this.Gb_AddReciteItem.Controls.Add(this.cb_Products);
            this.Gb_AddReciteItem.Location = new System.Drawing.Point(6, 191);
            this.Gb_AddReciteItem.Name = "Gb_AddReciteItem";
            this.Gb_AddReciteItem.Size = new System.Drawing.Size(248, 203);
            this.Gb_AddReciteItem.TabIndex = 1;
            this.Gb_AddReciteItem.TabStop = false;
            this.Gb_AddReciteItem.Text = "AddReciteItem";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.Location = new System.Drawing.Point(7, 145);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(229, 38);
            this.btnAddItem.TabIndex = 6;
            this.btnAddItem.Text = "AddItem";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.BtnAddItem_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(-3, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "numericUnitPrice: ";
            // 
            // numeric_UnitPrice
            // 
            this.numeric_UnitPrice.Location = new System.Drawing.Point(126, 102);
            this.numeric_UnitPrice.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numeric_UnitPrice.Name = "numeric_UnitPrice";
            this.numeric_UnitPrice.Size = new System.Drawing.Size(109, 20);
            this.numeric_UnitPrice.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(-2, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "numericQuantity : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-4, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Product : ";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // numeric_Quantity
            // 
            this.numeric_Quantity.Location = new System.Drawing.Point(127, 68);
            this.numeric_Quantity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numeric_Quantity.Name = "numeric_Quantity";
            this.numeric_Quantity.Size = new System.Drawing.Size(109, 20);
            this.numeric_Quantity.TabIndex = 1;
            // 
            // cb_Products
            // 
            this.cb_Products.FormattingEnabled = true;
            this.cb_Products.Location = new System.Drawing.Point(115, 41);
            this.cb_Products.Name = "cb_Products";
            this.cb_Products.Size = new System.Drawing.Size(121, 21);
            this.cb_Products.TabIndex = 0;
            this.cb_Products.Text = "SelectProduct";
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(13, 439);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(229, 38);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // HeaderRecite
            // 
            this.HeaderRecite.Controls.Add(this.lblReceiptDate);
            this.HeaderRecite.Controls.Add(this.lblReceiptType);
            this.HeaderRecite.Controls.Add(this.lblReceiptNumber);
            this.HeaderRecite.Location = new System.Drawing.Point(13, 12);
            this.HeaderRecite.Name = "HeaderRecite";
            this.HeaderRecite.Size = new System.Drawing.Size(241, 173);
            this.HeaderRecite.TabIndex = 3;
            this.HeaderRecite.TabStop = false;
            this.HeaderRecite.Text = "HeaderRecite";
            // 
            // lblReceiptDate
            // 
            this.lblReceiptDate.AutoSize = true;
            this.lblReceiptDate.Location = new System.Drawing.Point(6, 48);
            this.lblReceiptDate.Name = "lblReceiptDate";
            this.lblReceiptDate.Size = new System.Drawing.Size(35, 13);
            this.lblReceiptDate.TabIndex = 2;
            this.lblReceiptDate.Text = "label4";
            // 
            // lblReceiptType
            // 
            this.lblReceiptType.AutoSize = true;
            this.lblReceiptType.Location = new System.Drawing.Point(6, 80);
            this.lblReceiptType.Name = "lblReceiptType";
            this.lblReceiptType.Size = new System.Drawing.Size(35, 13);
            this.lblReceiptType.TabIndex = 1;
            this.lblReceiptType.Text = "label4";
            // 
            // lblReceiptNumber
            // 
            this.lblReceiptNumber.AutoSize = true;
            this.lblReceiptNumber.Location = new System.Drawing.Point(6, 16);
            this.lblReceiptNumber.Name = "lblReceiptNumber";
            this.lblReceiptNumber.Size = new System.Drawing.Size(35, 13);
            this.lblReceiptNumber.TabIndex = 0;
            this.lblReceiptNumber.Text = "label4";
            // 
            // btn_Updateitem
            // 
            this.btn_Updateitem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Updateitem.Location = new System.Drawing.Point(13, 400);
            this.btn_Updateitem.Name = "btn_Updateitem";
            this.btn_Updateitem.Size = new System.Drawing.Size(229, 33);
            this.btn_Updateitem.TabIndex = 7;
            this.btn_Updateitem.Text = "UpdateItem";
            this.btn_Updateitem.UseVisualStyleBackColor = true;
            this.btn_Updateitem.Click += new System.EventHandler(this.Btn_Updateitem_Click);
            // 
            // EditReciteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 489);
            this.Controls.Add(this.btn_Updateitem);
            this.Controls.Add(this.HeaderRecite);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.Gb_AddReciteItem);
            this.Controls.Add(this.dataGridViewItems);
            this.Name = "EditReciteForm";
            this.Text = "EditReciteForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warehouseReceiptItemsBindingSource)).EndInit();
            this.Gb_AddReciteItem.ResumeLayout(false);
            this.Gb_AddReciteItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_UnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Quantity)).EndInit();
            this.HeaderRecite.ResumeLayout(false);
            this.HeaderRecite.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.GroupBox Gb_AddReciteItem;
        private System.Windows.Forms.ComboBox cb_Products;
        private System.Windows.Forms.NumericUpDown numeric_Quantity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numeric_UnitPrice;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.GroupBox HeaderRecite;
        private System.Windows.Forms.Label lblReceiptNumber;
        private System.Windows.Forms.Label lblReceiptDate;
        private System.Windows.Forms.Label lblReceiptType;
        private System.Windows.Forms.BindingSource warehouseReceiptItemsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn receiptIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalAmountDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btn_Updateitem;
    }
}