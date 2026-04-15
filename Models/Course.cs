using System.Collections.Generic;
using System.Linq;

namespace UniversitetConsoleApp.Models
{
    public class Course
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public int Credits { get; private set; }
        public int MaxStudents { get; private set; }
        public Teacher Teacher { get; private set; }
        public List<Student> Students { get; private set; }
        public List<LibraryItem> Curriculum { get; private set; }

        public Course(string code, string name, int credits, int maxStudents, Teacher teacher)
        {
            Code = code;
            Name = name;
            Credits = credits;
            MaxStudents = maxStudents;
            Teacher = teacher;
            Students = new List<Student>();
            Curriculum = new List<LibraryItem>();
        }

        public bool AddStudent(Student student)
        {
            if (Students.Any(s => s.StudentId == student.StudentId))
                return false;

            if (Students.Count >= MaxStudents)
                return false;

            Students.Add(student);

            if (!student.EnrolledCourses.Any(c => c.Code == Code))
            {
                student.EnrolledCourses.Add(this);
            }

            return true;
        }

        public bool RemoveStudent(Student student)
        {
            bool removed = Students.Remove(student);

            if (removed)
            {
                student.EnrolledCourses.RemoveAll(c => c.Code == Code);
            }

            return removed;
        }

        public void AddCurriculumBook(LibraryItem item)
        {
            if (!Curriculum.Any(b => b.Id == item.Id))
            {
                Curriculum.Add(item);
            }
        }
    }
}

    
