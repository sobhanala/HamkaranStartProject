using Domain.Common;
using System;

namespace AnbarDomain.Partys
{
    public class Party : BaseEntity
    {
        public string Name { get; set; }
        public PartyType PartyType { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}