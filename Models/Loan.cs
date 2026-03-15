using System;


namespace UniversitetConsoleApp.Models
{
     public class Loan
    {
        public User Borrower {get; set; }
        public LibraryItem Item { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Loan(User borrower, LibraryItem item)
        {
            Borrower = borrower;
            Item = item;
            LoanDate = DateTime.Now;
        }
        
    }
}
        

