using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Module;

namespace Shell.forms
{
    public partial class ModuleDashboardForm : Form
    {
        private readonly ModuleLoader _moduleLoader;
        private List<IModule> _modules;

        public ModuleDashboardForm(ModuleLoader moduleLoader)
        {
            _moduleLoader = moduleLoader;
            InitializeComponent();

        }

        private void ModuleDashboardForm_Load(object sender, EventArgs e)
        {
            if (_modules==null)
            {
                ModulesAndAction.Nodes.Add("Hi");
                return;

            }
            foreach (var module in _modules)
            {
                ModulesAndAction.Nodes.Add(module.Name,module.Description);
            }


        }
    }
}
