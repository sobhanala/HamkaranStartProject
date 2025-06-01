using Domain.Common;
using Domain.Permissons;
using System.Collections.Generic;

namespace Domain.Module
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}