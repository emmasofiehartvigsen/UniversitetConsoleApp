namespace UniversitetConsoleApp.Models
{
    public class Employee : User
    {
        public int EmployeeId { get; private set; }
        public string Position { get; private set; }
        public string Department { get; private set; }

        public Employee(int employeeId, string name, string email, string username, string password, string position, string department, UserRole role)
            : base(name, email, username, password, role)
        {
            EmployeeId = employeeId;
            Position = position;
            Department = department;
        }
    }
}
        
    
