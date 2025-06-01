using Domain.Module;
using Domain.SharedSevices;
using Infrastructure;
using System.Windows.Forms;

namespace AdminForm
{

    public class AdminModule : IModule
    {
        private readonly IUserService _userManagement;

        public AdminModule(IUserService userManagement)
        {
            _userManagement = userManagement;
        }

        public int Id => PasswordHasher.GetDeterministicHashCode(Subname);

        public string Name => "Admin";
        public string Subname => "this module is for Admin to assign roles ";

        public void Initialize()
        {
        }


        public Form GetMainForm()
        {
            return new forms.AdminForm(_userManagement);
        }
    }

}