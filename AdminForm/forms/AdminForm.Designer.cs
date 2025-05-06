namespace AdminForm.forms
{
    partial class AdminForm
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
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.clbModules = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblSelectionCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUsers
            // 
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(12, 12);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(797, 448);
            this.dgvUsers.TabIndex = 1;
            this.dgvUsers.DoubleClick += new System.EventHandler(this.DgvUsers_DoubleClick);
            // 
            // clbModules
            // 
            this.clbModules.CheckOnClick = true;
            this.clbModules.FormattingEnabled = true;
            this.clbModules.Location = new System.Drawing.Point(660, 48);
            this.clbModules.Name = "clbModules";
            this.clbModules.Size = new System.Drawing.Size(120, 379);
            this.clbModules.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(30, 427);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(211, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Assignments";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btn_SaveClicked);
            // 
            // lblSelectionCount
            // 
            this.lblSelectionCount.AutoSize = true;
            this.lblSelectionCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectionCount.Location = new System.Drawing.Point(825, 12);
            this.lblSelectionCount.Name = "lblSelectionCount";
            this.lblSelectionCount.Size = new System.Drawing.Size(201, 31);
            this.lblSelectionCount.TabIndex = 4;
            this.lblSelectionCount.Text = "Selected users ";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 472);
            this.Controls.Add(this.lblSelectionCount);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.clbModules);
            this.Controls.Add(this.dgvUsers);
            this.Name = "AdminForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.CheckedListBox clbModules;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSelectionCount;
    }
}