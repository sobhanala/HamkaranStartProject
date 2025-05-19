using System;
using System.Windows.Forms;
using Application;
using Domain.Exceptions;
using Domain.SharedSevices;
using Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Shell.forms
{
    public partial class LoginForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;


        public LoginForm(IUserService userService, IServiceProvider serviceProvider, ISessionService sessionService)
        {
            _userService = userService;
            _serviceProvider = serviceProvider;
            _sessionService = sessionService;
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

                var result = await _userService.LoginUser(textBox1.Text, textBox2.Text);

                SetupSession(result);

                       
                DialogResult = DialogResult.OK;
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
            catch (Exception)
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



        private void SetupSession(User u)
        {
            _sessionService.Initialize(u);
            var moduleForm = _serviceProvider.GetService<ModuleDashboardForm>();
            moduleForm.Show();
        }
    }
}