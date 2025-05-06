namespace AnbarForm.MainForm
{
    partial class UserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.lbl_HelpText = new System.Windows.Forms.Label();
            this.tbl_moduleShow = new System.Windows.Forms.TableLayoutPanel();
            this.pic_ModuleAddParty = new System.Windows.Forms.PictureBox();
            this.tbl_moduleShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ModuleAddParty)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_HelpText
            // 
            this.lbl_HelpText.AutoSize = true;
            this.lbl_HelpText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_HelpText.Location = new System.Drawing.Point(39, 30);
            this.lbl_HelpText.Name = "lbl_HelpText";
            this.lbl_HelpText.Size = new System.Drawing.Size(168, 26);
            this.lbl_HelpText.TabIndex = 0;
            this.lbl_HelpText.Text = "Select an Ability";
            this.lbl_HelpText.Click += new System.EventHandler(this.Label1_Click);
            // 
            // tbl_moduleShow
            // 
            this.tbl_moduleShow.ColumnCount = 2;
            this.tbl_moduleShow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_moduleShow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_moduleShow.Controls.Add(this.pic_ModuleAddParty, 0, 0);
            this.tbl_moduleShow.Location = new System.Drawing.Point(44, 76);
            this.tbl_moduleShow.Name = "tbl_moduleShow";
            this.tbl_moduleShow.RowCount = 2;
            this.tbl_moduleShow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_moduleShow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_moduleShow.Size = new System.Drawing.Size(429, 291);
            this.tbl_moduleShow.TabIndex = 1;
            // 
            // pic_ModuleAddParty
            // 
            this.pic_ModuleAddParty.Image = ((System.Drawing.Image)(resources.GetObject("pic_ModuleAddParty.Image")));
            this.pic_ModuleAddParty.Location = new System.Drawing.Point(3, 3);
            this.pic_ModuleAddParty.Name = "pic_ModuleAddParty";
            this.pic_ModuleAddParty.Size = new System.Drawing.Size(208, 139);
            this.pic_ModuleAddParty.TabIndex = 0;
            this.pic_ModuleAddParty.TabStop = false;
            this.pic_ModuleAddParty.Click += new System.EventHandler(this.Pic_ModuleAddParty_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1185, 564);
            this.Controls.Add(this.tbl_moduleShow);
            this.Controls.Add(this.lbl_HelpText);
            this.Name = "UserForm";
            this.Text = "AnbarForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tbl_moduleShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_ModuleAddParty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_HelpText;
        private System.Windows.Forms.TableLayoutPanel tbl_moduleShow;
        private System.Windows.Forms.PictureBox pic_ModuleAddParty;
    }
}