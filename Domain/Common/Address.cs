namespace Domain.Common
{
    public class Address
    {
        public string Street;

        public Address(string street, string city, string state, string postalCode, string country)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }
    }
}