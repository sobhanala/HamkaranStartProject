using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class Address
    {
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }
        public string Street;
        public Address(string street, string city, string state, string postalCode, string country)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
