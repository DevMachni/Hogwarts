using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hogwarts.Data;
using Hogwarts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using static System.Console;
namespace Hogwarts
{
    class Program
    {
        static HogwartsContext Context = new HogwartsContext();
        static void Main()
        {
            bool shouldRun = true;
            while (shouldRun)
            {
                Clear();
                WriteLine("1. Registrera elev");
                WriteLine("2. Lista elever");
                WriteLine("3. Lägg till lärare");
                WriteLine("4. Lägg till kurs");
                WriteLine("5. Lägg till elev till kurs");

                ConsoleKeyInfo keyPressed = ReadKey(true);
                Clear();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                        RegisterStudent();
                        break;
                    case ConsoleKey.D2:
                        DisplayStudent();
                        break;
                    case ConsoleKey.D3:
                        RegisterTeacher();
                        break;
                    case ConsoleKey.D4:
                        RegisterCourse();
                        break;
                    case ConsoleKey.D5:
                        RegisterStudentToCourse();
                        break;
                    case ConsoleKey.D6:
                        DisplayStudentsAndCourses();
                        break;
                }
            }

        }

        private static void DisplayStudentsAndCourses()
        {
            List<Course> courseList = Context.Course
            .Include(x => x.Teacher)
            .Include(student => student.StudentCourse)
            .ThenInclude(x => x.Student).ToList();

            foreach (var course in courseList)
            {
                WriteLine($"Titel: {course.Title}");
                WriteLine($"Beskrivning: {course.Description}");
                WriteLine($"Poäng: {course.Points}");
                WriteLine($"Lärare: {course.Teacher.FirstName} {course.Teacher.LastName}");

                WriteLine("Elever:");
                foreach (var student in course.StudentCourse)
                {
                    WriteLine($"{student.Student.FirstName} {student.Student.LastName}");
                }
                WriteLine("---------------------------------");
            }
            EscToExit();
        }

        private static void RegisterStudentToCourse()
        {
            {
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Elev (personnr):");
                    string socialSecurityNumber = ReadLine();
                    
                    WriteLine();

                    WriteLine("Kurs (titel):");
                    string title = ReadLine();
                 
                    WriteLine();

                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Clear();
                                Course course = Context
                                    .Course
                                    .FirstOrDefault(x => x.Title == title);
                                Student student = Context
                                    .Student
                                    .FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);
                                    if (student == null)
                                    {
                                        WriteLine("Elev hittades ej");
                                    }
                                    else if (course == null)
                                    {
                                        WriteLine("Kursen hittades ej");
                                    }
                                    else
                                    {
                                        CourseStudent studentCourse = new CourseStudent(student.Id);
                                        course.StudentCourse.Add(studentCourse);
                                        Context.SaveChanges();
                                        WriteLine("Elev tillagd till kurs");

                                    }
                                    Thread.Sleep(2000); 
                                    break;

                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }

        private static void RegisterCourse()
        {
            {
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Titel:");
                    string title = ReadLine();
                    WriteLine();

                    WriteLine("Beskrivning:");
                    string description = ReadLine();
                    WriteLine();

                    WriteLine("Poäng:");
                    int points = int.Parse(ReadLine());
                    WriteLine();

                    WriteLine("Lärare (person.nmr):");
                    string socialSecurityNumber = ReadLine();
                    WriteLine();


                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Teacher teacher = Context.Teacher.FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);
                                Clear();
                     
                       
                                if (teacher != null)
                                {
                                    if (Context.Course.Any(x => x.Title == title))
                                    {
                                        WriteLine("Kurs redan tillagd");
                                    }
                                    else
                                    {
                                        Course course = new Course(title, description, points);
                                        teacher.Course.Add(course);
                                        Context.SaveChanges();
                                        WriteLine("Kurs tillagd");
                                    }
                                }
                                else
                                {
                                    WriteLine("Ogiltig lärare");
                                }
                                Thread.Sleep(2000);
                                break;

                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }

        private static void DisplayStudent()
        {
            List<Student> studentList = Context.Student.Include(x => x.Address).ToList();
            WriteLine("Namn\t\t\t\t\tAdress");
            WriteLine("------------------------------------------------------------");
            foreach (var student in studentList)
            {
                WriteLine($"{student.FirstName} {student.LastName}, {student.SocialSecurityNumber}              {student.Address.Street}, {student.Address.Postcode} {student.Address.City}");
            }

            EscToExit();
        }

        private static void RegisterTeacher()
        {
            {
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Förnamn:");
                    string firstName = ReadLine();
                    WriteLine();

                    WriteLine("Efternamn:");
                    string lastName = ReadLine();
                    WriteLine();

                    WriteLine("Personnummer:");
                    string socialSecurityNumber = ReadLine();
                    WriteLine();

                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Teacher teacher = Context.Teacher.FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);
                                Clear();
                                if (teacher != null)
                                {
                                    WriteLine("Lärare redan tillagd");
                                }
                                else
                                {
                                    Teacher newTeacher = new Teacher(firstName, lastName, socialSecurityNumber);
                                    Context.Teacher.Add(newTeacher);
                                    Context.SaveChanges();
                                    WriteLine("Lärare tillagd");
                                }
                                Thread.Sleep(2000);
                                break;

                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }
        
        private static void EscToExit()
        {
            ConsoleKeyInfo keyPressed;
            bool escapePressed = false;

            do
            {
                keyPressed = ReadKey(true);

                escapePressed |= keyPressed.Key == ConsoleKey.Escape;

            } while (!escapePressed);
        }

        private static void RegisterStudent()
        {
            {
                bool shouldAbort = false;
                do
                {
                    Clear();

                    WriteLine("Förnamn:");
                    string firstName = ReadLine();
                    WriteLine();

                    WriteLine("Efternamn:");
                    string lastName = ReadLine();
                    WriteLine();

                    WriteLine("Personnummer:");
                    string socialSecurityNumber = ReadLine();
                    WriteLine();

                    WriteLine("Gata:");
                    string street = ReadLine();
                    WriteLine();

                    WriteLine("Stad:");
                    string city = ReadLine();
                    WriteLine();

                    WriteLine("Postnummer:");
                    int postcode = int.Parse(ReadLine());
                    WriteLine();

                    WriteLine("Är detta korrekt? (J)a eller (N)ej");

                    bool hasInvalidInput = true;

                    do
                    {
                        ConsoleKeyInfo keyPressed = ReadKey(true);
                        switch (keyPressed.Key)
                        {
                            case ConsoleKey.J:
                                hasInvalidInput = false;
                                shouldAbort = true;
                                Clear();
                                Student students = Context.Student.FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);
                                if (students != null)
                                {
                                    WriteLine("Elev redan registrerad");
                                }
                                else
                                {
                                    Address address = new Address(street, city , postcode);
                                    Student student = new Student(firstName, lastName , socialSecurityNumber, address);
                                    Context.Student.Add(student);
                                    Context.SaveChanges();
                                    WriteLine("Elev registrerad");
                                }
                                Thread.Sleep(2000);
                                break;

                            case ConsoleKey.N:
                                hasInvalidInput = false;
                                break;
                        }
                    } while (hasInvalidInput);
                } while (shouldAbort == false);
            }
        }
    }
}
