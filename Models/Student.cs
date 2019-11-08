using System;
using System.Collections.Generic;

namespace Hogwarts.Domain.Models
{
    class Student
    {
 
        public Student(string firstName, string lastName, string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public Student(string firstName, string lastName, string socialSecurityNumber, Address address) : this(firstName, lastName, socialSecurityNumber)
        {
            Address = address;
        }

        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string SocialSecurityNumber { get; protected set; }
        public Address Address { get; protected set; }
        public List<CourseStudent> StudentCourse { get; protected set; } = new List<CourseStudent>();
    }
}
