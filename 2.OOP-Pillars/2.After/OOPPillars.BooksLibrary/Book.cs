﻿namespace CentricExpress.OOPPillars.BooksLibrary.Models
{
    public abstract class Book
    {
        private Guid _id { get; set; }
        public Author[] Authors { get; set; }
        public string Title { get; set; }
        public string ISBN { get; private set; }

        protected Book(Author[] authors, string title)
        {
            Authors = authors;
            Title = title;
        }

        #region static polimorphysm, or compile-type  polimorphysm
        public virtual void GenerateISBN()
        {
            _id = Guid.NewGuid();
            ISBN = $"PUB-{_id}";
        }

        public virtual void GenerateISBN(Guid id)
        {
            _id = id;
            ISBN = $"PUB-{id}";
        }
        #endregion

        #region dynamic, or run-time polimorphysm
        public abstract string AccessContent(string username);
        #endregion

    }
}
