using System;
using Domain.Common;

namespace AnbarDomain.Partys
{
    public class Party : BaseEntity
    {
        public string Name { get; set; }
        public string PartyType { get; set; } 
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TaxId { get; set; }
        public decimal CreditLimit { get; set; }
        public string PaymentTerms { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? UpdatedBy { get; set; }
    }
}