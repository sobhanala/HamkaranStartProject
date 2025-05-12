namespace Shell.forms
{
    partial class ModuleDashboardForm
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
            this.ModulesAndAction = new System.Windows.Forms.TreeView();
            this.lbl_Username = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ModulesAndAction
            // 
            this.ModulesAndAction.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ModulesAndAction.Location = new System.Drawing.Point(11, 65);
            this.ModulesAndAction.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ModulesAndAction.Name = "ModulesAndAction";
            this.ModulesAndAction.Size = new System.Drawing.Size(583, 347);
            this.ModulesAndAction.TabIndex = 0;
            // 
            // lbl_Username
            // 
            this.lbl_Username.AutoSize = true;
            this.lbl_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Username.Location = new System.Drawing.Point(285, 9);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(0, 49);
            this.lbl_Username.TabIndex = 1;
            this.lbl_Username.UseCompatibleTextRendering = true;
            // 
            // ModuleDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 420);
            this.Controls.Add(this.lbl_Username);
            this.Controls.Add(this.ModulesAndAction);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ModuleDashboardForm";
            this.Text = "ModuleDashbordForm";
            this.Load += new System.EventHandler(this.ModuleDashboardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView ModulesAndAction;
        private System.Windows.Forms.Label lbl_Username;
    }
}