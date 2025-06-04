using Domain.Exceptions;
using Domain.SharedSevices;
using Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using Infrastructure;

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

                var result = await UiSafeExecutor.ExecuteAsync(() => _userService.LoginUser(textBox1.Text, textBox2.Text));

                SetupSession(result);


                DialogResult = DialogResult.OK;
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

            moduleForm.FormClosed += (s, e) =>
            {
                Dispose(); 
            };

            moduleForm.Show();
            Hide();
        }
    }
}