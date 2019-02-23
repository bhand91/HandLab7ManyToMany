using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HandLab7ManyToMany
{
    class Program
    {
        static void List()
        {
            using (var db = new SchoolDbContext())
            {
                var lists = db.Courses.Include (c => c.StudentCourses).ThenInclude(sc => sc.Student);
                foreach (var course in lists)
                {
                    Console.WriteLine($"{course.CourseName} -");
                    foreach (var student in course.StudentCourses)
                    {
                        Console.WriteLine($"\t{student.Student.FirstName} {student.Student.LastName} - {student.GPA}");
                    }
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            using (var db = new SchoolDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                List<Course> courses = new List<Course>()
                {
                    new Course {CourseName = "English"},
                    new Course {CourseName = "Math"},
                };

                db.AddRange(courses);
                db.SaveChanges();

                List<Student> students = new List<Student>
                {
                    new Student {FirstName = "John", LastName = "Stewart"},
                    new Student {FirstName = "Trevor", LastName = "Noah"},
                    new Student {FirstName = "Kevin", LastName = "Hart"},
                    new Student {FirstName = "Will", LastName = "Ferrell"},
                };

                db.AddRange(students);
                db.SaveChanges();

                List<StudentCourse> studentCourses = new List<StudentCourse>
                {
                    new StudentCourse {Student = students[0], Course = courses[0], GPA = 2.9M},
                    new StudentCourse {Student = students[0], Course = courses[1], GPA = 4.0M},
                    new StudentCourse {Student = students[1], Course = courses[0], GPA = 1.8M},
                    new StudentCourse {Student = students[2], Course = courses[1], GPA = 3.2M},
                    new StudentCourse {Student = students[3], Course = courses[1], GPA = 2.8M},
                    new StudentCourse {Student = students[2], Course = courses[0], GPA = 3.5M},
                };
                db.AddRange(studentCourses);
                db.SaveChanges();

                List();

                Student newStudent = new Student {FirstName = "Ali", LastName = "Wong"};

                db.Add(newStudent);
                db.SaveChanges();
                
                StudentCourse updateSC = new StudentCourse {
                        Student = db.Students.Find(5),
                        Course = db.Courses.Find(2),
                        GPA = 3.8M,
                };
                
                db.Add(updateSC);
                db.SaveChanges();

                List();
            }
        }
    }
}
