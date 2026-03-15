namespace UniversitetConsoleApp.Models
{
    public class LibraryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Copies { get; set; }
        public int AvailableCopies { get; set; }

        public LibraryItem(int id, string title, string author, int year, int copies)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Copies = copies;
            AvailableCopies = copies;
        }
    }
}
        
    
