namespace AnbarForm.MainForm.Reciteforms.selectors
{
    partial class WarehouseSelectorForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_select = new System.Windows.Forms.Label();
            this.lbl_search = new System.Windows.Forms.Label();
            this.serachbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 28);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(824, 150);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // lbl_select
            // 
            this.lbl_select.AutoSize = true;
            this.lbl_select.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lbl_select.Location = new System.Drawing.Point(0, 0);
            this.lbl_select.Name = "lbl_select";
            this.lbl_select.Size = new System.Drawing.Size(150, 18);
            this.lbl_select.TabIndex = 1;
            this.lbl_select.Text = "Select a Warehouse: ";
            // 
            // lbl_search
            // 
            this.lbl_search.AutoSize = true;
            this.lbl_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lbl_search.Location = new System.Drawing.Point(415, 5);
            this.lbl_search.Name = "lbl_search";
            this.lbl_search.Size = new System.Drawing.Size(99, 18);
            this.lbl_search.TabIndex = 2;
            this.lbl_search.Text = "SearchName:";
            // 
            // serachbox
            // 
            this.serachbox.Location = new System.Drawing.Point(573, 2);
            this.serachbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.serachbox.Name = "serachbox";
            this.serachbox.Size = new System.Drawing.Size(239, 22);
            this.serachbox.TabIndex = 3;
            this.serachbox.TextChanged += new System.EventHandler(this.Textbox_TextChanged);
            // 
            // WarehouseSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 178);
            this.Controls.Add(this.serachbox);
            this.Controls.Add(this.lbl_search);
            this.Controls.Add(this.lbl_select);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "WarehouseSelectorForm";
            this.Text = "WarehouseSelectorForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_select;
        private System.Windows.Forms.Label lbl_search;
        private System.Windows.Forms.TextBox serachbox;
    }
}