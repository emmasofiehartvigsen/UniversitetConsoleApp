namespace UniversitetConsoleApp.Models
{
    public class LibraryItem
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int Year { get; private set; }
        public int Copies { get; private set; }
        public int AvailableCopies { get; private set; }

        public LibraryItem(int id, string title, string author, int year, int copies)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Copies = copies;
            AvailableCopies = copies;
        }

        public bool LoanOut()
        {
            if (AvailableCopies <= 0)
                return false;

            AvailableCopies--;
            return true;
        }

        public void ReturnCopy()
        {
            if (AvailableCopies < Copies)
            {
                AvailableCopies++;
            }
        }
    }
}
        
    
