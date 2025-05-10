namespace AnbarForm.MainForm
{
    partial class ProductManagementForm
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
            this.btn_save_all = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.anbarDataSet1 = new AnbarDomain.Tabels.AnbarDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anbarDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_save_all
            // 
            this.btn_save_all.Location = new System.Drawing.Point(108, 344);
            this.btn_save_all.Name = "btn_save_all";
            this.btn_save_all.Size = new System.Drawing.Size(243, 80);
            this.btn_save_all.TabIndex = 21;
            this.btn_save_all.Text = "Save All";
            this.btn_save_all.UseVisualStyleBackColor = true;
            this.btn_save_all.Click += new System.EventHandler(this.Btn_save_all_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(446, 326);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // anbarDataSet1
            // 
            this.anbarDataSet1.DataSetName = "AnbarDataSet";
            this.anbarDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ProductManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 436);
            this.Controls.Add(this.btn_save_all);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ProductManagementForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anbarDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_save_all;
        private System.Windows.Forms.DataGridView dataGridView1;
        private AnbarDomain.Tabels.AnbarDataSet anbarDataSet1;
    }
}