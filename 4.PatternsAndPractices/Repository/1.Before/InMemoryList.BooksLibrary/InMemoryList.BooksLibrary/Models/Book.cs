﻿namespace CentricExpress.BooksLibrary.Models
{
    public class Book
    {
        //do I need it?? - 

        //public Guid Id { get; private set; }
        //public string ISBN { get; set; }
        public Author[] Authors { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }

        public Book(Author[] authors, string title, string publisher)
        {
            //Id = Guid.NewGuid();

            Authors = authors;
            Title = title;
            Publisher = publisher;
        }

        //public void GenerateId()
        //{
        //    if (Id == Guid.Empty)
        //    {
        //        Id = Guid.NewGuid();
        //    }
        //}

    }
}
