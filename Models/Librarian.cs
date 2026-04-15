namespace UniversitetConsoleApp.Models
{
    public class Librarian : Employee
    {
        public Librarian(int employeeId, string name, string email, string username, string password)
            : base(employeeId, name, email, username, password, "Librarian", "Library", UserRole.Librarian)
        {
        }
    }
}
