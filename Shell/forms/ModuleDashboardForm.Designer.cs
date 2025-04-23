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
            this.SuspendLayout();
            // 
            // ModulesAndAction
            // 
            this.ModulesAndAction.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ModulesAndAction.Location = new System.Drawing.Point(12, 12);
            this.ModulesAndAction.Name = "ModulesAndAction";
            this.ModulesAndAction.Size = new System.Drawing.Size(776, 426);
            this.ModulesAndAction.TabIndex = 0;
            // 
            // ModuleDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ModulesAndAction);
            this.Name = "ModuleDashboardForm";
            this.Text = "ModuleDashbordForm";
            this.Load += new System.EventHandler(this.ModuleDashboardForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ModulesAndAction;
    }
}