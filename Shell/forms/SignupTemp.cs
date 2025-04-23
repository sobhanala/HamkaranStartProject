using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application;
using Domain.Exceptions;

namespace Shell.forms
{
    public partial class SignupTemp : Form
    {
        private readonly IUserService _userService;

        public SignupTemp(IUserService userService)
        {
            _userService = userService;
            InitializeComponent();
        }

        private async void Signupbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please fill in all fields", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Signupbtn.Enabled = false;
                Signupbtn.Text = "Processing...";

                var success = await _userService.AddUser(textBox1.Text, textBox2.Text);

                if (success)
                {
                    MessageBox.Show($"Account '{textBox1.Text}' created successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.UserFriendlyMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.UserFriendlyMessage, "Registration Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred during registration", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Signupbtn.Enabled = true;
                Signupbtn.Text = "Sign Up";
            }
        }
    }
}
