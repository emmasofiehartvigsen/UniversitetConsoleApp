namespace UniversitetConsoleApp.Models
{
    public class ExchangeStudent : Student
    {
        public string HomeUniversity { get; set; }
        public string Country { get; set; }
        public string Period { get; set; }

        public ExchangeStudent(int studentId, string name, string email, string homeUniversity, string country, string period)
            : base(studentId, name, email)
        {
            HomeUniversity = homeUniversity;
            Country = country;
            Period = period;
        }
    }
}
        