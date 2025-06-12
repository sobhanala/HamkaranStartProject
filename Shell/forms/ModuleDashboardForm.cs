using Domain.Module;
using Domain.SharedSevices;
using Domain.Users;
using System;
using System.Linq;
using System.Windows.Forms;


namespace Shell.forms
{
    public partial class ModuleDashboardForm : Form
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;

        public ModuleDashboardForm(IUserService userService, ISessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
            InitializeComponent();
            ModulesAndAction.NodeMouseClick += OnModuleNodeClicked;

        }

        private async void ModuleDashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (ModuleManager.Modules == null)
                {
                    return;
                }

                lbl_Username.Text = _sessionService.CurrentUser.Username;
                var permissions = await _userService.ShowAllUserPermission(userId: _sessionService.CurrentUser.Id);
                var permittedModuleIds = permissions.Select(p => p.ModuleId).ToHashSet();

                var groupedModules = ModuleManager.Modules.GroupBy(m => m.Name);

                foreach (var group in groupedModules)
                {
                    var parentNode = new TreeNode
                    {
                        Text = group.Key
                    };

                    foreach (var module in group)
                    {
                        if (!permittedModuleIds.Contains(module.Id) && _sessionService.CurrentUser.Role != Roles.Admin)
                            continue;

                        var childNode = new TreeNode
                        {
                            Tag = module,
                            Text = module.Subname
                        };

                        parentNode.Nodes.Add(childNode);
                    }

                    if (parentNode.Nodes.Count > 0)
                    {
                        ModulesAndAction.Nodes.Add(parentNode);
                    }
                }

            }
            catch (Exception exception)
            {

                MessageBox.Show($"Error loading module: {exception.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void OnModuleNodeClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!(e.Node?.Tag is IModule module)) return;

            try
            {
                var moduleForm = module.GetMainForm();
                if (moduleForm != null)
                {
                    moduleForm.Show();

                }

                module.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading module: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}