namespace CentricExpress.OOPPillars.BooksLibrary.Models
{
    /// <summary>
    /// SpecialCollection books are books with a historical value, that can be accessed by specific members (ex university staff, muzelogists, etc)
    /// </summary>
    public class SpecialCollectionBook : Book
    {
        private IList<string> _allowedMembers = new List<string>();
      
        public SpecialCollectionBook(Author[] authors, string title) : base(authors, title)
        {
            _allowedMembers = new List<string>
            {
                "EGrigorescu",
                "NScrimint",
                "HMalancus",
                "RGramescu"
            };
        }

        /// <summary>
        /// Accessing a book from Special Collection means having some specific member-rights
        /// </summary>
        /// <param name="memberName"></param>       
        public override string AccessContent(string memberName)
        {
            //check memebr subscription list
            if (!_allowedMembers.Contains(memberName, StringComparer.OrdinalIgnoreCase))
                return $"Access denied for '{Title}'";

            return $"You are allowed to access books from Special collection. '{Title}' can be found at Floor 5, row 2, 2nd schelv";
        }
    }
}
