using Domain.Module;
using Domain.SharedSevices;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infrastructure;

namespace AdminForm.forms
{
    public partial class AdminForm : Form
    {
        private readonly IUserService _userService;
        private User _selectedUser = null;
        private List<int> _selctedModules = new List<int>();
        private DataGridViewRow _lastSelectedRow = null;





        public AdminForm(IUserService userService)
        {
            _userService = userService;
            InitializeComponent();

        }


        private async void AdminForm_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadUsers();
                LoadModules();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }


        private void LoadModules()
        {

            clbModules.Items.Clear();
            foreach (var module in ModuleManager.Modules)
            {
                clbModules.Items.Add(module.Name, false);
            }
        }

        private async Task LoadUsers()
        {
            var users = await UiSafeExecutor.ExecuteAsync(()=>  _userService.GetAllUsers());
            dgvUsers.DataSource = users;

            // Hide sensitive columns
            dgvUsers.Columns["Id"].Visible = false;
            dgvUsers.Columns["PasswordHash"].Visible = false;


        }

        private async void DgvUsers_DoubleClick(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            var selectedUser = dgvUsers.CurrentRow.DataBoundItem as User;

            if (_selectedUser == selectedUser)
            {
                _selectedUser = null;
                lblSelectionCount.Text = "No user selected";
                ClearModuleSelections();
            }
            else
            {
                _selectedUser = selectedUser;
                lblSelectionCount.Text = $"Selected: {_selectedUser.Username}";
                await LoadUserModules();
            }

            if (_lastSelectedRow != null)
            {
                _lastSelectedRow.DefaultCellStyle.BackColor = Color.White;
            }
            dgvUsers.CurrentRow.DefaultCellStyle.BackColor = Color.LightBlue;
            _lastSelectedRow = dgvUsers.CurrentRow;
        }

        private async Task LoadUserModules()
        {
            if (_selectedUser == null) return;

            try
            {
                var userModules = await UiSafeExecutor.ExecuteAsync( ()=> _userService.ShowAllUserPermission(_selectedUser.Id));
                _selctedModules = userModules.Select(p => p.ModuleId).ToList();

                for (int i = 0; i < clbModules.Items.Count; i++)
                {
                    var module = ModuleManager.Modules[i];
                    clbModules.SetItemChecked(i, _selctedModules.Contains(module.Id));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading modules: {ex.Message}");
            }
        }

        private void ClearModuleSelections()
        {
            for (int i = 0; i < clbModules.Items.Count; i++)
            {
                clbModules.SetItemChecked(i, false);
            }
            _selctedModules.Clear();
        }


        private async void btn_SaveClicked(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Please select a user first");
                return;
            }

            try
            {
                var selectedIds = new List<int>();
                for (int i = 0; i < clbModules.Items.Count; i++)
                {
                    if (clbModules.GetItemChecked(i))
                    {

                        var module = ModuleManager.Modules[i];
                        selectedIds.Add(i);
                        // Optionally display the name (for debug)
                        MessageBox.Show($"Checked: {module.Name}");
                    }
                }

                await UiSafeExecutor.ExecuteAsync( async()=>await _userService.AddPermissionToUser(selectedIds, _selectedUser.Id));
                MessageBox.Show("Modules updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving modules: {ex.Message}");
            }
        }

    }
}
