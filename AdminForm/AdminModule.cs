﻿using System.Windows.Forms;
using Domain.Module;
using Domain.SharedSevices;
using Infrastructure;

namespace AdminForm
{

    public class AdminModule : IModule
    {
        private readonly IUserService _userManagement;

        public AdminModule(IUserService userManagement)
        {
            _userManagement = userManagement;
        }

        public string Name => "Admin";
        public string Description => "this module is for Admin to assign roles ";

        public void Initialize()
        {
        }

        public int Id => PasswordHasher.GetDeterministicHashCode(Name);

        public Form GetMainForm()
        {
            return new forms.AdminForm(_userManagement);
        }
    }

}