namespace AnbarForm.MainForm.Reciteforms
{
    partial class ViewForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.masterGrid = new System.Windows.Forms.DataGridView();
            this.detailGrid = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.masterGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.masterGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.detailGrid, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(759, 308);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // masterGrid
            // 
            this.masterGrid.AllowUserToAddRows = false;
            this.masterGrid.AllowUserToDeleteRows = false;
            this.masterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.masterGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterGrid.Location = new System.Drawing.Point(3, 3);
            this.masterGrid.Name = "masterGrid";
            this.masterGrid.ReadOnly = true;
            this.masterGrid.Size = new System.Drawing.Size(753, 86);
            this.masterGrid.TabIndex = 0;
            // 
            // detailGrid
            // 
            this.detailGrid.AllowUserToAddRows = false;
            this.detailGrid.AllowUserToDeleteRows = false;
            this.detailGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailGrid.Location = new System.Drawing.Point(3, 95);
            this.detailGrid.Name = "detailGrid";
            this.detailGrid.ReadOnly = true;
            this.detailGrid.Size = new System.Drawing.Size(753, 210);
            this.detailGrid.TabIndex = 1;
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 308);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ViewForm";
            this.Text = "ViewForm";
            this.Load += new System.EventHandler(this.ViewForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.masterGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView masterGrid;
        private System.Windows.Forms.DataGridView detailGrid;
    }
}