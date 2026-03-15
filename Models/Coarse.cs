using System.Collections.Generic;

namespace UniversitetConsoleApp.Models
{
    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int MaxStudents { get; set; }
        public List<Student> Students { get; set; }

        public Course(string code, string name, int credits, int maxStudents)
        {
            Code = code;
            Name = name;
            Credits = credits;
            MaxStudents = maxStudents;
            Students = new List<Student>();
        }
    }
}
        
    
