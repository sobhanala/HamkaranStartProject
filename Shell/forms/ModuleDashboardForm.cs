using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Application;
using Domain.Module;


namespace Shell.forms
{
    public partial class ModuleDashboardForm : Form
    {
        private readonly ModuleLoader _moduleLoader;

        public ModuleDashboardForm(ModuleLoader moduleLoader)
        {
            _moduleLoader = moduleLoader;
            InitializeComponent();
            ModulesAndAction.NodeMouseClick += OnModuleNodeClicked;

        }

        private void ModuleDashboardForm_Load(object sender, EventArgs e)
        {
            if (ModuleManager.Modules == null)
            {
                ModulesAndAction.Nodes.Add("Hi");
                return;
            }

            var groupedModules = ModuleManager.Modules.GroupBy(m => m.Name);

            foreach (var group in groupedModules)
            {
                var parentNode = new TreeNode
                {
                    Text = group.Key 
                };

                foreach (var module in group)
                {
                    var childNode = new TreeNode
                    {
                        Tag = module,
                        Text = module.Description 
                    };
                    parentNode.Nodes.Add(childNode);
                }

                ModulesAndAction.Nodes.Add(parentNode);
            }

        }
        private void OnModuleNodeClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            var module = e.Node?.Tag as IModule;
            if (module == null) return;

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