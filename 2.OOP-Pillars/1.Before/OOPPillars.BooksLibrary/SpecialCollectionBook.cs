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
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override string AccessContent(string memberName)
        {
            //TODO: check if the given memberName is part of the _allowedMembers list
            //if yes, return a positive answer: ex: "You are allowe to access book.Title .."
            //if no, return a nagative answer, ex "Access denied"

            return string.Empty;
        }
    }
}
