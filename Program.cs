using System;
using System.Collections.Generic;
using UniversitetConsoleApp.Models;
using UniversitetConsoleApp.Services;

namespace UniversitetConsoleApp
{
    public enum StartMenuChoice
    {
        Exit = 0,
        ExistingUser = 1,
        NewUser = 2
    }

    class Program
    {
        static void Main(string[] args)
        {
            UniversitySystem system = new UniversitySystem();
            SeedData(system);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== UNIVERSITY SYSTEM =====");
                Console.WriteLine("[1] Eksisterende bruker");
                Console.WriteLine("[2] Ny bruker");
                Console.WriteLine("[0] Avslutt");

                int startChoice = ReadInt("Velg et alternativ: ");

                switch ((StartMenuChoice)startChoice)
                {
                    case StartMenuChoice.ExistingUser:
                        LoginFlow(system);
                        break;

                    case StartMenuChoice.NewUser:
                        RegisterFlow(system);
                        break;

                    case StartMenuChoice.Exit:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }
            }
        }

        static void RegisterFlow(UniversitySystem system)
        {
            Console.WriteLine("\nVelg rolle:");
            Console.WriteLine("[1] Student");
            Console.WriteLine("[2] Faglærer");
            Console.WriteLine("[3] Bibliotekansatt");

            int roleChoice = ReadInt("Velg rolle: ");

            Console.Write("Navn: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("E-post: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Brukernavn: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Passord: ");
            string password = Console.ReadLine() ?? "";

            string result = "";

            switch (roleChoice)
            {
                case 1:
                    int studentId = ReadInt("Student-ID: ");
                    result = system.RegisterStudent(studentId, name, email, username, password);
                    break;

                case 2:
                    int teacherId = ReadInt("Ansatt-ID: ");
                    result = system.RegisterTeacher(teacherId, name, email, username, password);
                    break;

                case 3:
                    int librarianId = ReadInt("Ansatt-ID: ");
                    result = system.RegisterLibrarian(librarianId, name, email, username, password);
                    break;

                default:
                    result = "Ugyldig rollevalg.";
                    break;
            }

            Console.WriteLine(result);
        }

        static void LoginFlow(UniversitySystem system)
        {
            Console.Write("Brukernavn: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Passord: ");
            string password = Console.ReadLine() ?? "";

            User? user = system.Login(username, password);

            if (user == null)
            {
                Console.WriteLine("Feil brukernavn eller passord.");
                return;
            }

            Console.WriteLine($"Innlogget som {user.Name} ({user.Role})");

            switch (user.Role)
            {
                case UserRole.Student:
                    ShowStudentMenu(system, (Student)user);
                    break;

                case UserRole.Teacher:
                    ShowTeacherMenu(system, (Teacher)user);
                    break;

                case UserRole.Librarian:
                    ShowLibrarianMenu(system, (Librarian)user);
                    break;
            }
        }

        static void ShowStudentMenu(UniversitySystem system, Student student)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== STUDENTMENY =====");
                Console.WriteLine("[1] Meld på kurs");
                Console.WriteLine("[2] Meld av kurs");
                Console.WriteLine("[3] Se mine kurs");
                Console.WriteLine("[4] Se karakter");
                Console.WriteLine("[5] Søk på bok");
                Console.WriteLine("[6] Lån bok");
                Console.WriteLine("[7] Returner bok");
                Console.WriteLine("[0] Logg ut");

                int choice = ReadInt("Velg: ");

                switch (choice)
                {
                    case 1:
                        Console.Write("Kurskode: ");
                        string enrollCode = Console.ReadLine() ?? "";
                        Console.WriteLine(system.EnrollStudentInCourse(student, enrollCode));
                        break;

                    case 2:
                        Console.Write("Kurskode: ");
                        string unenrollCode = Console.ReadLine() ?? "";
                        Console.WriteLine(system.UnenrollStudentFromCourse(student, unenrollCode));
                        break;

                    case 3:
                        List<Course> courses = system.GetCoursesForStudent(student);
                        if (courses.Count == 0)
                        {
                            Console.WriteLine("Ingen påmeldte kurs.");
                        }
                        else
                        {
                            foreach (Course course in courses)
                            {
                                Console.WriteLine($"{course.Code} - {course.Name}");
                            }
                        }
                        break;

                    case 4:
                        Console.Write("Kurskode: ");
                        string gradeCode = Console.ReadLine() ?? "";
                        Console.WriteLine(student.GetGrade(gradeCode));
                        break;

                    case 5:
                        Console.Write("Søk etter bok: ");
                        string studentBookSearch = Console.ReadLine() ?? "";
                        List<LibraryItem> foundBooksStudent = system.SearchBooks(studentBookSearch);
                        PrintBooks(foundBooksStudent);
                        break;

                    case 6:
                        int loanBookId = ReadInt("Bok-ID: ");
                        Console.WriteLine(system.LoanBook(student, loanBookId));
                        break;

                    case 7:
                        int returnBookId = ReadInt("Bok-ID: ");
                        Console.WriteLine(system.ReturnBook(student, returnBookId));
                        break;

                    case 0:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }
            }
        }

        static void ShowTeacherMenu(UniversitySystem system, Teacher teacher)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== LÆRERMENY =====");
                Console.WriteLine("[1] Opprett kurs");
                Console.WriteLine("[2] Søk på kurs");
                Console.WriteLine("[3] Søk på bok");
                Console.WriteLine("[4] Lån bok");
                Console.WriteLine("[5] Returner bok");
                Console.WriteLine("[6] Sett karakter");
                Console.WriteLine("[7] Registrer pensum");
                Console.WriteLine("[0] Logg ut");

                int choice = ReadInt("Velg: ");

                switch (choice)
                {
                    case 1:
                        Console.Write("Kurskode: ");
                        string code = Console.ReadLine() ?? "";

                        Console.Write("Kursnavn: ");
                        string name = Console.ReadLine() ?? "";

                        int credits = ReadInt("Studiepoeng: ");
                        int maxStudents = ReadInt("Maks antall studenter: ");

                        Console.WriteLine(system.CreateCourse(code, name, credits, maxStudents, teacher));
                        break;

                    case 2:
                        Console.Write("Søk etter kurs: ");
                        string teacherCourseSearch = Console.ReadLine() ?? "";
                        List<Course> foundCourses = system.SearchCourses(teacherCourseSearch);

                        if (foundCourses.Count == 0)
                        {
                            Console.WriteLine("Ingen kurs funnet.");
                        }
                        else
                        {
                            foreach (Course course in foundCourses)
                            {
                                Console.WriteLine($"{course.Code} - {course.Name}");
                            }
                        }
                        break;

                    case 3:
                        Console.Write("Søk etter bok: ");
                        string teacherBookSearch = Console.ReadLine() ?? "";
                        List<LibraryItem> books = system.SearchBooks(teacherBookSearch);
                        PrintBooks(books);
                        break;

                    case 4:
                        int loanBookId = ReadInt("Bok-ID: ");
                        Console.WriteLine(system.LoanBook(teacher, loanBookId));
                        break;

                    case 5:
                        int returnBookId = ReadInt("Bok-ID: ");
                        Console.WriteLine(system.ReturnBook(teacher, returnBookId));
                        break;

                    case 6:
                        Console.Write("Kurskode: ");
                        string gradeCourseCode = Console.ReadLine() ?? "";
                        int studentId = ReadInt("Student-ID: ");
                        Console.Write("Karakter: ");
                        string grade = Console.ReadLine() ?? "";

                        Console.WriteLine(system.SetGrade(teacher, gradeCourseCode, studentId, grade));
                        break;

                    case 7:
                        Console.Write("Kurskode: ");
                        string curriculumCourseCode = Console.ReadLine() ?? "";
                        int curriculumBookId = ReadInt("Bok-ID: ");

                        Console.WriteLine(system.AddCurriculumToCourse(teacher, curriculumCourseCode, curriculumBookId));
                        break;

                    case 0:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }
            }
        }

        static void ShowLibrarianMenu(UniversitySystem system, Librarian librarian)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== BIBLIOTEKMENY =====");
                Console.WriteLine("[1] Registrer bok");
                Console.WriteLine("[2] Se aktive lån");
                Console.WriteLine("[3] Se historikk");
                Console.WriteLine("[0] Logg ut");

                int choice = ReadInt("Velg: ");

                switch (choice)
                {
                    case 1:
                        int id = ReadInt("Bok-ID: ");

                        Console.Write("Tittel: ");
                        string title = Console.ReadLine() ?? "";

                        Console.Write("Forfatter: ");
                        string author = Console.ReadLine() ?? "";

                        int year = ReadInt("År: ");
                        int copies = ReadInt("Antall kopier: ");

                        Console.WriteLine(system.RegisterBook(id, title, author, year, copies));
                        break;

                    case 2:
                        List<Loan> activeLoans = system.GetActiveLoans();
                        PrintLoans(activeLoans);
                        break;

                    case 3:
                        List<Loan> history = system.GetLoanHistory();
                        PrintLoans(history);
                        break;

                    case 0:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ugyldig valg.");
                        break;
                }
            }
        }

        static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                Console.WriteLine("Ugyldig input. Skriv inn et heltall.");
            }
        }

        static void PrintBooks(List<LibraryItem> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Ingen bøker funnet.");
                return;
            }

            foreach (LibraryItem book in books)
            {
                Console.WriteLine($"{book.Id} - {book.Title} av {book.Author} ({book.Year}) Tilgjengelig: {book.AvailableCopies}/{book.Copies}");
            }
        }

        static void PrintLoans(List<Loan> loans)
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("Ingen lån funnet.");
                return;
            }

            foreach (Loan loan in loans)
            {
                string status = loan.IsActive ? "Aktivt lån" : $"Returnert {loan.ReturnDate}";
                Console.WriteLine($"{loan.Borrower.Name} lånte '{loan.Item.Title}' den {loan.LoanDate}. Status: {status}");
            }
        }

        static void SeedData(UniversitySystem system)
        {
            system.RegisterTeacher(100, "Anne Lærer", "anne@uni.no", "anne", "1234");
            system.RegisterLibrarian(200, "Per Bibliotek", "per@uni.no", "per", "1234");
            system.RegisterStudent(1, "Ola Nordmann", "ola@uni.no", "ola", "1234");

            system.RegisterBook(1, "C# Basics", "Jon Doe", 2020, 3);
            system.RegisterBook(2, "Advanced LINQ", "Jane Doe", 2021, 2);
        }
    }
}