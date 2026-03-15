namespace UniversitetConsoleApp.Models
{
    public class Employee : User
    {
        public int EmployeeId { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

        public Employee(int employeeId, string name, string email, string position, string department)
            : base(name, email)
        {
            EmployeeId = employeeId;
            Position = position;
            Department = department;
        }
    }
}
        
    
