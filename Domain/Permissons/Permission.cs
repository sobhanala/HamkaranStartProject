using Domain.Common;
using Domain.Users;
using System;

namespace Domain.Permissons
{
    public class Permission : BaseEntity
    {
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public Authority Authority { get; set; }
        public DateTime CreatedAt { get; set; }

        public Module.Module Module { get; set; }
        public User User { get; set; }
    }
}