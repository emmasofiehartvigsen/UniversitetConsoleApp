namespace UniversitetConsoleApp.Models
{
    public class ExchangeStudent : Student
    {
        public string HomeUniversity { get; private set; }
        public string Country { get; private set; }
        public string Period { get; private set; }

        public ExchangeStudent(
            int studentId,
            string name,
            string email,
            string username,
            string password,
            string homeUniversity,
            string country,
            string period)
            : base(studentId, name, email, username, password)
        {
            HomeUniversity = homeUniversity;
            Country = country;
            Period = period;
        }
    }
}
        