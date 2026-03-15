using System;
using System.Collections.Generic;
using System.Linq;
using UniversitetConsoleApp.Models;

namespace UniversitetConsoleApp.Services
{
    public class UniversitySystem
    {
        public List<Student> Students { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Course> Courses { get; set; }
        public List<LibraryItem> LibraryItems { get; set; }
        public List<Loan> Loans { get; set; }

        public UniversitySystem()
        {
            Students = new List<Student>();
            Employees = new List<Employee>();
            Courses = new List<Course>();
            LibraryItems = new List<LibraryItem>();
            Loans = new List<Loan>();
        }

        public void CreateCourse()
        {
            Console.Write("Course code: ");
            string code = Console.ReadLine() ?? "";

            Console.Write("Course name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Credits: ");
            int credits = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Max students: ");
            int maxStudents = int.Parse(Console.ReadLine() ?? "0");

            Course course = new Course(code, name, credits, maxStudents);
            Courses.Add(course);

            Console.WriteLine("Course created successfully!");
        }

        public void EnrollStudentInCourse()
        {
            Console.Write("Student ID: ");
            int studentId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Course code: ");
            string code = Console.ReadLine() ?? "";

            Student student = Students.FirstOrDefault(s => s.StudentId == studentId);
            Course course = Courses.FirstOrDefault(c => c.Code == code);

            if (student != null && course != null)
            {
                if (course.Students.Count < course.MaxStudents)
                {
                    course.Students.Add(student);
                    student.EnrolledCourses.Add(course);

                    Console.WriteLine("Student enrolled successfully in course!");
                }
                else
                {
                    Console.WriteLine("Unfortunately, the course is full.");
                }
            }
            else
            {
                Console.WriteLine("Student or course not found.");
            }
        }

        public void PrintCoursesAndParticipants()
        {
            if (Courses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            foreach (var course in Courses)
            {
                Console.WriteLine($"\nCourse: {course.Code} - {course.Name}");
                Console.WriteLine($"Credits: {course.Credits}");
                Console.WriteLine($"Students: {course.Students.Count}/{course.MaxStudents}");

                if (course.Students.Count == 0)
                {
                    Console.WriteLine("No participants.");
                }
                else
                {
                    foreach (var student in course.Students)
                    {
                        Console.WriteLine($"- {student.Name} (ID: {student.StudentId})");
                    }
                }
            }
        }

        public void SearchCourse()
        {
            Console.Write("Search by course code or name: ");
            string search = Console.ReadLine() ?? "";

            var results = Courses.Where(c =>
                c.Code.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No courses found.");
                return;
            }

            foreach (var course in results)
            {
                Console.WriteLine($"{course.Code} - {course.Name}");
            }
        }

        public void RegisterBook()
        {
            Console.Write("Book ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Author: ");
            string author = Console.ReadLine() ?? "";

            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Number of copies: ");
            int copies = int.Parse(Console.ReadLine() ?? "0");

            LibraryItems.Add(new LibraryItem(id, title, author, year, copies));

            Console.WriteLine("Book registered successfully.");
        }

        public void SearchBook()
        {
            Console.Write("Search book title: ");
            string search = Console.ReadLine() ?? "";

            var results = LibraryItems.Where(b =>
                b.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            foreach (var item in results)
            {
                Console.WriteLine($"{item.Id} - {item.Title} by {item.Author} | Available: {item.AvailableCopies}");
            }
        }

        public void LoanBook()
        {
            Console.Write("Borrower type (1 = Student, 2 = Employee): ");
            string type = Console.ReadLine() ?? "";

            User borrower = null;

            if (type == "1")
            {
                Console.Write("Student ID: ");
                int studentId = int.Parse(Console.ReadLine() ?? "0");
                borrower = Students.FirstOrDefault(s => s.StudentId == studentId);
            }
            else if (type == "2")
            {
                Console.Write("Employee ID: ");
                int employeeId = int.Parse(Console.ReadLine() ?? "0");
                borrower = Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            }

            if (borrower == null)
            {
                Console.WriteLine("Borrower not found.");
                return;
            }

            Console.Write("Book ID: ");
            int bookId = int.Parse(Console.ReadLine() ?? "0");

            LibraryItem item = LibraryItems.FirstOrDefault(b => b.Id == bookId);

            if (item == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (item.AvailableCopies <= 0)
            {
                Console.WriteLine("No copies available.");
                return;
            }

            item.AvailableCopies--;
            Loans.Add(new Loan(borrower, item));

            Console.WriteLine("Book loaned successfully.");
        }

        public void ReturnBook()
        {
            Console.Write("Book ID: ");
            int bookId = int.Parse(Console.ReadLine() ?? "0");

            Loan activeLoan = Loans.FirstOrDefault(l => l.Item.Id == bookId && l.ReturnDate == null);

            if (activeLoan == null)
            {
                Console.WriteLine("No active loan found for this book.");
                return;
            }

            activeLoan.ReturnDate = DateTime.Now;
            activeLoan.Item.AvailableCopies++;

            Console.WriteLine("Book returned successfully.");
        }
    }
}
    



 
       
    
