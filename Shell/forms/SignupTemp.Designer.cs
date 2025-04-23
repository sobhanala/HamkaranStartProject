namespace Shell.forms
{
    partial class SignupTemp
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
            this.label4 = new System.Windows.Forms.Label();
            this.Signupbtn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(86, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 15;
            this.label4.Visible = false;
            // 
            // Signupbtn
            // 
            this.Signupbtn.Location = new System.Drawing.Point(89, 83);
            this.Signupbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Signupbtn.Name = "Signupbtn";
            this.Signupbtn.Size = new System.Drawing.Size(75, 23);
            this.Signupbtn.TabIndex = 14;
            this.Signupbtn.Text = "Signup";
            this.Signupbtn.UseVisualStyleBackColor = true;
            this.Signupbtn.Click += new System.EventHandler(this.Signupbtn_Click);
            // 
            // textBox2
            // 
            this.textBox2.AccessibleDescription = "Password text box in the login form";
            this.textBox2.AccessibleName = "PasswordTextBox ";
            this.textBox2.Location = new System.Drawing.Point(89, 44);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(295, 22);
            this.textBox2.TabIndex = 11;
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "password label in the login form";
            this.label2.AccessibleName = "PassWordLabel";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Password";
            // 
            // textBox1
            // 
            this.textBox1.AccessibleDescription = "iser name text box in the login form";
            this.textBox1.AccessibleName = "UserNameTextBox ";
            this.textBox1.Location = new System.Drawing.Point(89, 11);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(295, 22);
            this.textBox1.TabIndex = 9;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AccessibleDescription = "in the login form";
            this.UsernameLabel.AccessibleName = "UserNameLabel";
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(9, 17);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(74, 16);
            this.UsernameLabel.TabIndex = 8;
            this.UsernameLabel.Text = "UserName";
            // 
            // SignupTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 218);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Signupbtn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.UsernameLabel);
            this.Name = "SignupTemp";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Signupbtn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label UsernameLabel;
    }
}