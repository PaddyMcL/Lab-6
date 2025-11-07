using Microsoft.EntityFrameworkCore;
using StudentClassLibrary;
using System.Collections.Generic;

namespace StudentMvcApp.Data
{
    public class StudentMvcDbContext : DbContext
    {
        public StudentMvcDbContext(DbContextOptions<StudentMvcDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.EmailAddress).IsRequired().HasMaxLength(150);
                entity.Property(s => s.Age).IsRequired();
            });

            // Configure Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.DepartmentName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.LecturerName).IsRequired().HasMaxLength(100);
            });

            // Many-to-many join table
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseStudent",
                    j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                    j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
                    j => j.HasKey("CourseId", "StudentId")
                );

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { ID = 1, Name = "Alice Johnson", Age = 21, EmailAddress = "alice.j@atu.ie" },
                new Student { ID = 2, Name = "Bob Wilson", Age = 23, EmailAddress = "bob.w@atu.ie" },
                new Student { ID = 3, Name = "Charlie Brown", Age = 20, EmailAddress = "charlie.b@atu.ie" },
                new Student { ID = 4, Name = "Diana Prince", Age = 22, EmailAddress = "diana.p@atu.ie" }
            );

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { ID = 1, Name = "Advanced Programming", DepartmentName = "Computer Science", LecturerName = "Dr. Murphy" },
                new Course { ID = 2, Name = "Data Structures", DepartmentName = "Computer Science", LecturerName = "Prof. O'Brien" },
                new Course { ID = 3, Name = "Web Technologies", DepartmentName = "Computer Science", LecturerName = "Dr. Kelly" }
            );

            // Seed many-to-many
            modelBuilder.Entity("CourseStudent").HasData(
                new { CourseId = 1, StudentId = 1 },
                new { CourseId = 1, StudentId = 2 },
                new { CourseId = 2, StudentId = 1 },
                new { CourseId = 2, StudentId = 3 },
                new { CourseId = 3, StudentId = 2 },
                new { CourseId = 3, StudentId = 4 }
            );
        }
    }
}
