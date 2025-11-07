using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentClassLibrary
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string LecturerName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
