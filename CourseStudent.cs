using System;
namespace Hogwarts.Domain.Models
{
    class CourseStudent
    {




        public CourseStudent(int studentId)
        {
            StudentId = studentId;
        }

        public Student Student { get; protected set; }
        public Course Course { get; protected set; }
        public int StudentId { get; protected set; }
        public int CourseId { get; protected set; }
    }
}
