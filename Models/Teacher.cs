namespace UniversitetConsoleApp.Models
{
    public class Teacher : Employee
    {
        public Teacher(int employeeId, string name, string email, string username, string password)
            : base(employeeId, name, email, username, password, "Teacher", "Academic", UserRole.Teacher)
        {
        }
    }
}