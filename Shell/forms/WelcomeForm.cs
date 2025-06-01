using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace Shell.forms
{
    public partial class WelcomeForm : Form
    {
        private readonly IServiceProvider _serviceProvider;


        public WelcomeForm(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }


        private void btnLogin_click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<LoginForm>();

            Hide();
            form.ShowDialog(form.Owner);
            Show();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<SignupTemp>();

            Hide();
            form.ShowDialog(form.Owner);
            Show();
        }
    }
}