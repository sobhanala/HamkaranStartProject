using System;
using Domain.Common;

namespace AnbarDomain.Warehouse
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public int? ManagerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }
}