using System;
using System.Collections.Generic;

namespace Hogwarts.Domain.Models
{
    class Course
    {
        public Course(string title, string description, int points)
        {
            Title = title;
            Description = description;
            Points = points;
        }
        public Course()
        {

        }
        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public int Points { get; protected set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        public List<CourseStudent> StudentCourse { get; protected set; } = new List<CourseStudent>();

    }
}
