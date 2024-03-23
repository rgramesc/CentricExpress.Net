﻿namespace BooksLibrary.Infrastructure.Contracts;

public class BookWithAuthorDataDto
{
    public Guid BookId { get; set; }

    public DateTime BookPublicationDate { get; set; }

    public string BookTitle { get; set; }

    public bool BookIsRemoved { get; set; }

    public Guid AuthorId { get; set; }

    public string AuthorFirstName { get; set; }

    public string AuthorLastName { get; set; }
}