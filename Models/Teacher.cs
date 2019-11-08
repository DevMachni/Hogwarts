using System;
using System.Collections.Generic;

namespace Hogwarts.Domain.Models
{
    class Teacher
    {
        public Teacher(string firstName, string lastName, string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public Teacher()
        {

        }
        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string SocialSecurityNumber { get; protected set; }
        public List<Course> Course { get; protected set; } = new List<Course>();
    }
}
