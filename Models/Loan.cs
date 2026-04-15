using System;

namespace UniversitetConsoleApp.Models
{
    public class Loan
    {
        public User Borrower { get; private set; }
        public LibraryItem Item { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        public bool IsActive => ReturnDate == null;

        public Loan(User borrower, LibraryItem item)
        {
            Borrower = borrower;
            Item = item;
            LoanDate = DateTime.Now;
        }

        public void ReturnItem()
        {
            if (ReturnDate != null)
                return;

            ReturnDate = DateTime.Now;
            Item.ReturnCopy();
        }
    }
}
        

