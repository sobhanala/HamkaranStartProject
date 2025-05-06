using System;
using System.Collections.Generic;
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

            foreach (var module in ModuleManager.Modules)
            {
                var node = new TreeNode
                {
                    Tag = module,
                    Text = module.Description
                };
                ModulesAndAction.Nodes.Add(node);
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