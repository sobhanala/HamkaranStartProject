namespace AnbarForm.MainForm.Reciteforms.selectors
{
    partial class BaseSelector
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
        protected void InitializeComponent()
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(600, 173);
            this.dataGridView1.TabIndex = 0;
            // 
            // lbl_search
            // 
            this.lbl_search.AutoSize = true;
            this.lbl_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lbl_search.Location = new System.Drawing.Point(241, 0);
            this.lbl_search.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_search.Name = "lbl_search";
            this.lbl_search.Size = new System.Drawing.Size(63, 18);
            this.lbl_search.TabIndex = 5;
            this.lbl_search.Text = "Search  ";
            // 
            // lbl_select
            // 
            this.lbl_select.AutoSize = true;
            this.lbl_select.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.lbl_select.Location = new System.Drawing.Point(0, 0);
            this.lbl_select.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_select.Name = "lbl_select";
            this.lbl_select.Size = new System.Drawing.Size(53, 18);
            this.lbl_select.TabIndex = 4;
            this.lbl_select.Text = "Select ";
            // 
            // searchbox
            // 
            this.searchbox.Location = new System.Drawing.Point(352, 0);
            this.searchbox.Margin = new System.Windows.Forms.Padding(2);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(224, 20);
            this.searchbox.TabIndex = 6;
            // 
            // BaseSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 197);
            this.Controls.Add(this.searchbox);
            this.Controls.Add(this.lbl_search);
            this.Controls.Add(this.lbl_select);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BaseSelector";
            this.Text = "BaseSelector";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_search;
        private System.Windows.Forms.Label lbl_select;
        protected System.Windows.Forms.TextBox searchbox;
    }
}