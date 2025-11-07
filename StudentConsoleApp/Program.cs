using Microsoft.EntityFrameworkCore;
using StudentClassLibrary;
using StudentConsoleApp;

// Create a new instance of the DbContext
using (var context = new StudentDbContext())
{
    // Ensure the database and tables are created
    context.Database.EnsureCreated();

    // Create courses
    var course1 = new Course
    {
        Name = "Web Development",
        DepartmentName = "Computer Science",
        LecturerName = "Dr. Smith",
        Students = new List<Student>()
    };

    var course2 = new Course
    {
        Name = "Database Systems",
        DepartmentName = "Computer Science",
        LecturerName = "Prof. Johnson",
        Students = new List<Student>()
    };

    // Create students
    var student1 = new Student
    {
        Name = "John Doe",
        Age = 20,
        EmailAddress = "john.doe@student.com",
        Courses = new List<Course>()
    };

    var student2 = new Student
    {
        Name = "Jane Smith",
        Age = 22,
        EmailAddress = "jane.smith@student.com",
        Courses = new List<Course>()
    };

    // Assign students to courses
    course1.Students.Add(student1);
    course1.Students.Add(student2);
    course2.Students.Add(student1);

    // Add courses to context (students are added via relationship)
    context.Courses.Add(course1);
    context.Courses.Add(course2);

    // Save all changes to the database
    context.SaveChanges();

    Console.WriteLine("Students and courses added successfully!");

    // Display courses with enrolled students
    Console.WriteLine("\n--- Courses and Students ---");
    foreach (var course in context.Courses.Include(c => c.Students).ToList())
    {
        Console.WriteLine($"\nCourse: {course.Name}");
        Console.WriteLine($"Lecturer: {course.LecturerName}");
        Console.WriteLine($"Number of students: {course.Students.Count}");
    }

    // Display students with enrolled courses
    Console.WriteLine("\n--- Students ---");
    foreach (var student in context.Students.Include(s => s.Courses).ToList())
    {
        Console.WriteLine($"\nStudent: {student.Name}");
        Console.WriteLine($"Age: {student.Age}");
        Console.WriteLine($"Email: {student.EmailAddress}");
    }
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
