using System;
namespace Hogwarts.Domain.Models
{
    class Address
    {
        public Address(string street, string city, int postcode)
        {
            Street = street;
            City = city;
            Postcode = postcode;
        }

        public int Id { get; protected set; }
        public string Street { get; protected set; }
        public string City { get; protected set; }
        public int Postcode { get; protected set; }
        public int StudentId { get; protected set; }
        public Student Student { get; protected set; }
    }
}
