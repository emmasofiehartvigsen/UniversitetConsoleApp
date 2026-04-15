namespace UniversitetConsoleApp.Models
{
    public abstract class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public UserRole Role { get; private set; }

        protected User(string name, string email, string username, string password, UserRole role)
        {
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            Role = role;
        }

        public bool CheckPassword(string password)
        {
            return Password == password;
        }
    }
}
        
    
