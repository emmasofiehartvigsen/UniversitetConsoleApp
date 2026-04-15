using System.Collections.Generic;
using System.Linq;

namespace UniversitetConsoleApp.Models
{
    public class Student : User
    {
        public int StudentId { get; private set; }
        public List<Course> EnrolledCourses { get; private set; }
        public Dictionary<string, string> Grades { get; private set; }

        public Student(int studentId, string name, string email, string username, string password)
            : base(name, email, username, password, UserRole.Student)
        {
            StudentId = studentId;
            EnrolledCourses = new List<Course>();
            Grades = new Dictionary<string, string>();
        }

        public bool EnrollInCourse(Course course)
        {
            if (EnrolledCourses.Any(c => c.Code == course.Code))
                return false;

            EnrolledCourses.Add(course);
            return true;
        }

        public bool LeaveCourse(Course course)
        {
            return EnrolledCourses.Remove(course);
        }

        public void SetGrade(string courseCode, string grade)
        {
            Grades[courseCode] = grade;
        }

        public string GetGrade(string courseCode)
        {
            return Grades.ContainsKey(courseCode) ? Grades[courseCode] : "Ingen karakter registrert";
        }
    }
}

        
    
