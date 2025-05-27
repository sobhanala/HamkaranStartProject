namespace AnbarForm.MainForm
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.nestedTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.grpAddItem = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_addItem = new System.Windows.Forms.Button();
            this.dgReciteItem = new System.Windows.Forms.DataGridView();
            this.btn_Save = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.grpHeader.SuspendLayout();
            this.grpAddItem.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReciteItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.grpHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpAddItem, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgReciteItem, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_Save, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.53448F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.46552F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1443, 627);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.TableLayoutPanel1_Paint);
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.nestedTableLayoutPanel);
            this.grpHeader.Location = new System.Drawing.Point(3, 3);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1437, 95);
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
            this.nestedTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nestedTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.nestedTableLayoutPanel.Name = "nestedTableLayoutPanel";
            this.nestedTableLayoutPanel.RowCount = 2;
            this.nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.nestedTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.nestedTableLayoutPanel.Size = new System.Drawing.Size(1431, 76);
            this.nestedTableLayoutPanel.TabIndex = 1;
            // 
            // grpAddItem
            // 
            this.grpAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAddItem.Controls.Add(this.flowLayoutPanel);
            this.grpAddItem.Location = new System.Drawing.Point(3, 104);
            this.grpAddItem.Name = "grpAddItem";
            this.grpAddItem.Size = new System.Drawing.Size(1437, 125);
            this.grpAddItem.TabIndex = 1;
            this.grpAddItem.TabStop = false;
            this.grpAddItem.Text = "Add Item";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.btn_addItem);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(1431, 106);
            this.flowLayoutPanel.TabIndex = 0;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // btn_addItem
            // 
            this.btn_addItem.Location = new System.Drawing.Point(3, 3);
            this.btn_addItem.Name = "btn_addItem";
            this.btn_addItem.Size = new System.Drawing.Size(20, 20);
            this.btn_addItem.TabIndex = 0;
            this.btn_addItem.Text = "+";
            this.btn_addItem.UseVisualStyleBackColor = true;
            // 
            // dgReciteItem
            // 
            this.dgReciteItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReciteItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgReciteItem.Location = new System.Drawing.Point(3, 235);
            this.dgReciteItem.Name = "dgReciteItem";
            this.dgReciteItem.Size = new System.Drawing.Size(1437, 261);
            this.dgReciteItem.TabIndex = 2;
            this.dgReciteItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgReciteItem_CellContentClick);
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Location = new System.Drawing.Point(3, 573);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(1437, 51);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
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
            this.grpAddItem.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgReciteItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox grpAddItem;
        private System.Windows.Forms.TableLayoutPanel nestedTableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button btn_addItem;
        private System.Windows.Forms.DataGridView dgReciteItem;
        private System.Windows.Forms.Button btn_Save;
    }
}