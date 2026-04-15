using System;
using System.Collections.Generic;
using System.Linq;
using UniversitetConsoleApp.Models;

namespace UniversitetConsoleApp.Services
{
    public class UniversitySystem
    {
        public List<User> Users { get; set; }
        public List<Student> Students { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Course> Courses { get; set; }
        public List<LibraryItem> LibraryItems { get; set; }
        public List<Loan> Loans { get; set; }

        public UniversitySystem()
        {
            Users = new List<User>();
            Students = new List<Student>();
            Employees = new List<Employee>();
            Courses = new List<Course>();
            LibraryItems = new List<LibraryItem>();
            Loans = new List<Loan>();
        }

        public string RegisterStudent(int studentId, string name, string email, string username, string password)
        {
            if (Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return "Brukernavnet finnes allerede.";

            Student student = new Student(studentId, name, email, username, password);
            Students.Add(student);
            Users.Add(student);

            return "Student registrert.";
        }

        public string RegisterTeacher(int employeeId, string name, string email, string username, string password)
        {
            if (Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return "Brukernavnet finnes allerede.";

            Teacher teacher = new Teacher(employeeId, name, email, username, password);
            Employees.Add(teacher);
            Users.Add(teacher);

            return "Faglærer registrert.";
        }

        public string RegisterLibrarian(int employeeId, string name, string email, string username, string password)
        {
            if (Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return "Brukernavnet finnes allerede.";

            Librarian librarian = new Librarian(employeeId, name, email, username, password);
            Employees.Add(librarian);
            Users.Add(librarian);

            return "Bibliotekansatt registrert.";
        }

        public User? Login(string username, string password)
        {
            User? user = Users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return null;

            if (!user.CheckPassword(password))
                return null;

            return user;
        }

        public string CreateCourse(string code, string name, int credits, int maxStudents, Teacher teacher)
        {
            bool exists = Courses.Any(c =>
                c.Code.Equals(code, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (exists)
                return "Kurs med samme kode eller navn finnes allerede.";

            Course course = new Course(code, name, credits, maxStudents, teacher);
            Courses.Add(course);

            return "Kurs opprettet.";
        }

        public List<Course> SearchCourses(string searchTerm)
        {
            return Courses.Where(c =>
                c.Code.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public string EnrollStudentInCourse(Student student, string courseCode)
        {
            Course? course = Courses.FirstOrDefault(c =>
                c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (course == null)
                return "Kurs ikke funnet.";

            bool success = course.AddStudent(student);

            if (!success)
                return "Studenten kan ikke meldes på kurset. Enten er kurset fullt eller studenten er allerede meldt på.";

            return "Student meldt på kurs.";
        }

        public string UnenrollStudentFromCourse(Student student, string courseCode)
        {
            Course? course = Courses.FirstOrDefault(c =>
                c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (course == null)
                return "Kurs ikke funnet.";

            bool success = course.RemoveStudent(student);

            if (!success)
                return "Studenten er ikke meldt på dette kurset.";

            return "Student meldt av kurs.";
        }

        public string RegisterBook(int id, string title, string author, int year, int copies)
        {
            if (LibraryItems.Any(b => b.Id == id))
                return "En bok med samme ID finnes allerede.";

            LibraryItem item = new LibraryItem(id, title, author, year, copies);
            LibraryItems.Add(item);

            return "Bok registrert.";
        }

        public List<LibraryItem> SearchBooks(string searchTerm)
        {
            return LibraryItems.Where(b =>
                b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public string LoanBook(User user, int bookId)
        {
            if (user.Role != UserRole.Student && user.Role != UserRole.Teacher)
                return "Denne brukeren kan ikke låne bøker.";

            LibraryItem? item = LibraryItems.FirstOrDefault(b => b.Id == bookId);

            if (item == null)
                return "Bok ikke funnet.";

            if (!item.LoanOut())
                return "Ingen tilgjengelige eksemplarer.";

            Loan loan = new Loan(user, item);
            Loans.Add(loan);

            return "Bok lånt ut.";
        }

        public string ReturnBook(User user, int bookId)
        {
            Loan? activeLoan = Loans.FirstOrDefault(l =>
                l.Borrower == user &&
                l.Item.Id == bookId &&
                l.IsActive);

            if (activeLoan == null)
                return "Fant ikke aktivt lån.";

            activeLoan.ReturnItem();
            return "Bok returnert.";
        }

        public List<Loan> GetActiveLoans()
        {
            return Loans.Where(l => l.IsActive).ToList();
        }

        public List<Loan> GetLoanHistory()
        {
            return Loans;
        }

        public string AddCurriculumToCourse(Teacher teacher, string courseCode, int bookId)
        {
            Course? course = Courses.FirstOrDefault(c =>
                c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (course == null)
                return "Kurs ikke funnet.";

            if (course.Teacher.EmployeeId != teacher.EmployeeId)
                return "Du underviser ikke dette kurset.";

            LibraryItem? book = LibraryItems.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
                return "Bok ikke funnet.";

            course.AddCurriculumBook(book);
            return "Pensum lagt til.";
        }

        public string SetGrade(Teacher teacher, string courseCode, int studentId, string grade)
        {
            Course? course = Courses.FirstOrDefault(c =>
                c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (course == null)
                return "Kurs ikke funnet.";

            if (course.Teacher.EmployeeId != teacher.EmployeeId)
                return "Du underviser ikke dette kurset.";

            Student? student = course.Students.FirstOrDefault(s => s.StudentId == studentId);

            if (student == null)
                return "Studenten finnes ikke i kurset.";

            student.SetGrade(course.Code, grade);
            return "Karakter satt.";
        }

        public List<Course> GetCoursesForStudent(Student student)
        {
            return student.EnrolledCourses;
        }
    }
} 
        
        
            
        
    

    



 
       
    
