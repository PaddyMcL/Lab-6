using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentClassLibrary
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string EmailAddress { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
