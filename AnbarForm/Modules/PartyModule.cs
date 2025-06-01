using AnbarService;
using Domain.Module;
using Infrastructure;
using System.Windows.Forms;

namespace AnbarForm.Modules
{

    public class PartyModule : IModule
    {
        private readonly IPartyManagement _partyManagement;

        public PartyModule(IPartyManagement partyManagement)
        {
            _partyManagement = partyManagement;
        }
        public int Id => PasswordHasher.GetDeterministicHashCode(Subname);
        public string Name => "Party Management";
        public string Subname => "Manage customers, vendors, and other business parties";

        public void Initialize()
        {
        }


        public Form GetMainForm()
        {
            return new MainForm.PartyManagementForm(_partyManagement);
        }
    }

}
