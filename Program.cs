using System;
using UniversitetConsoleApp.Services;

namespace UniversitetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UniversitySystem system = new UniversitySystem();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== UNIVERSITY SYSTEM =====");
                Console.WriteLine("[1] Opprett kurs");
                Console.WriteLine("[2] Meld student til kurs");
                Console.WriteLine("[3] Print kurs og deltagere");
                Console.WriteLine("[4] Søk på kurs");
                Console.WriteLine("[5] Søk på bok");
                Console.WriteLine("[6] Lån bok");
                Console.WriteLine("[7] Returner bok");
                Console.WriteLine("[8] Registrer bok");
                Console.WriteLine("[0] Avslutt");
                Console.Write("Velg et alternativ: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        system.CreateCourse();
                        break;

                    case "2":
                        system.EnrollStudentInCourse();
                        break;

                    case "3":
                        system.PrintCoursesAndParticipants();
                        break;

                    case "4":
                        system.SearchCourse();
                        break;

                    case "5":
                        system.SearchBook();
                        break;

                    case "6":
                        system.LoanBook();
                        break;

                    case "7":
                        system.ReturnBook();
                        break;

                    case "8":
                        system.RegisterBook();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Program ended.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}

