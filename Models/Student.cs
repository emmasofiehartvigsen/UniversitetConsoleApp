
using System.Collections.Generic;
namespace UniversitetConsoleApp.Models
{
    public class Student : User
    {
        public int StudentId { get; set; }
        public List<Course> EnrolledCourses { get; set; }

        public Student(int studentId, string name, string email)
            : base(name, email)
        {
            StudentId = studentId;
            EnrolledCourses = new List<Course>();
        }
    }
}
        
    
