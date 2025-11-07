using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using StudentClassLibrary;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudentConsoleApp
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // IMPORTANT: Change this path to a location on YOUR machine
            options.UseSqlite($"Data Source=C:\\Temp\\students.db");
        }
    }
}