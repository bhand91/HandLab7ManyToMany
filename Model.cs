using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HandLab7ManyToMany
{
    public class SchoolDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = database.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(s => new {s.StudentId, s.CourseId });
        }

        public DbSet<Student> Students {get; set;}

        public DbSet<Course> Courses {get; set;}

        public DbSet<StudentCourse> StudentCourses {get; set;}
    }

    public class Student
    {
        public int StudentId {get; set;}

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public List<StudentCourse> StudentCourses {get; set;} //Navigation Property. Student can have many StudentCourses
    }

    public class Course
    {
        public int CourseId {get; set;}

        public string CourseName {get; set;}

        public List<StudentCourse> StudentCourses {get; set;} //Navigation property. Course can have many studentCourses
    }

    public class StudentCourse
    {
        public int StudentId {get; set;} //Composite primary key

        public int CourseId {get; set;} //Composite primary key

        public decimal GPA {get; set;}

        public Student Student {get; set;} //navigation property. One student per studentcourse

        public Course Course {get; set;} //navigation property. One course per studentcourse
    }
}