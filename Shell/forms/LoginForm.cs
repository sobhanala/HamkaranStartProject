using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application;
using Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Shell.forms
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;

        public LoginForm(IUserService userService, IServiceProvider serviceProvider)
        {
            _userService = userService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
        }



        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please fill in all fields", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                button1.Enabled = false;
                button1.Text = "Processing...";

                var result = await _userService.LoginUser(name: textBox1.Text, password: textBox2.Text);

                if (result == null) return;


                var moduleDashboard = _serviceProvider.GetRequiredService<ModuleDashboardForm>();
                moduleDashboard.Show();

                this.DialogResult = DialogResult.OK;

            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.UserFriendlyMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.UserFriendlyMessage, "Authentication Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred during registration", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            finally
            {
                button1.Enabled = true;
                button1.Text = "Login";
            }

        }


    }
}