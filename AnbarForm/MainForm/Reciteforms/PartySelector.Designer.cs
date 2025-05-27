namespace AnbarForm.MainForm.Reciteforms
{
    partial class PartySelector
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
            this.lbl_search = new System.Windows.Forms.Label();
            this.lbl_select = new System.Windows.Forms.Label();
            this.searchbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 30);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(800, 213);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentDoubleClick);
            // 
            // lbl_search
            // 
            this.lbl_search.AutoSize = true;
            this.lbl_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lbl_search.Location = new System.Drawing.Point(321, 0);
            this.lbl_search.Name = "lbl_search";
            this.lbl_search.Size = new System.Drawing.Size(126, 24);
            this.lbl_search.TabIndex = 5;
            this.lbl_search.Text = "SearchName:";
            // 
            // lbl_select
            // 
            this.lbl_select.AutoSize = true;
            this.lbl_select.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lbl_select.Location = new System.Drawing.Point(0, 0);
            this.lbl_select.Name = "lbl_select";
            this.lbl_select.Size = new System.Drawing.Size(190, 24);
            this.lbl_select.TabIndex = 4;
            this.lbl_select.Text = "Select a Warehouse: ";
            // 
            // searchbox
            // 
            this.searchbox.Location = new System.Drawing.Point(454, 2);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(298, 22);
            this.searchbox.TabIndex = 6;
            this.searchbox.TextChanged += new System.EventHandler(this.Searchbox_TextChanged);
            // 
            // PartySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 243);
            this.Controls.Add(this.searchbox);
            this.Controls.Add(this.lbl_search);
            this.Controls.Add(this.lbl_select);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PartySelector";
            this.Text = "PartySelector";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_search;
        private System.Windows.Forms.Label lbl_select;
        private System.Windows.Forms.TextBox searchbox;
    }
}