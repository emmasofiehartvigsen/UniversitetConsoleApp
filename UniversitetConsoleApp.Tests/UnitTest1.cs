using Xunit;
using UniversitetConsoleApp.Models;
using UniversitetConsoleApp.Services;

namespace UniversitetConsoleApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Student_Cannot_Enroll_Twice()
        {
            Teacher teacher = new Teacher(1, "Anne", "anne@test.no", "anne", "1234");
            Student student = new Student(1, "Ola", "ola@test.no", "ola", "1234");

            Course course = new Course("CS101", "Programmering", 10, 30, teacher);

            bool first = course.AddStudent(student);
            bool second = course.AddStudent(student);

            Assert.True(first);
            Assert.False(second);
        }

        [Fact]
        public void Course_Cannot_Exceed_MaxStudents()
        {
            Teacher teacher = new Teacher(1, "Anne", "anne@test.no", "anne", "1234");

            Course course = new Course("CS101", "Programmering", 10, 1, teacher);

            Student s1 = new Student(1, "Ola", "ola@test.no", "ola", "1234");
            Student s2 = new Student(2, "Kari", "kari@test.no", "kari", "1234");

            bool first = course.AddStudent(s1);
            bool second = course.AddStudent(s2);

            Assert.True(first);
            Assert.False(second);
        }

        [Fact]
        public void Course_Starts_Empty()
        {
            Teacher teacher = new Teacher(1, "Anne", "anne@test.no", "anne", "1234");

            Course course = new Course("CS101", "Programmering", 10, 30, teacher);

            Assert.Empty(course.Students);
        }
    }
}
