namespace CentricExpress.OOPPillars.BooksLibrary.Models
{
    /// <summary>
    /// Public books are books that can be accessed/borrowed by any member of the library
    /// </summary>
    public class PublicBook : Book
    {
        /// <summary>
        /// true if the book is reserved to be borrowed
        /// </summary>
        private bool _isReserved { get; set; }
        /// <summary>
        /// member name that reserved the book at the shelv
        /// </summary>
        private string? _reservedByMember { get; set; }

        public PublicBook(Author[] authors, string title) : base(authors, title)
        {
            _isReserved = false;
        }
      
        public override string AccessContent(string memberName)
        {
            //TODO: check if the book can be reserved
            //if yes, set _isReserved to true, and _reservedByMember to memberName
            //if no, return a nagative answer, ex "Book has already been reserved"

            return string.Empty;
        }
    }
}
