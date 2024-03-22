using CentricExpress.OOPPillars.BooksLibrary.Models;

Console.WriteLine("Hello to OOP Pillars!");

//create an empty list of books:
IList<Book> books = new List<Book>();

//create add the PublicBook in the list
var publicAuthors = new Author[]
{
    new Author("Martin", "Fowler")
};

Book publicBook = new PublicBook(publicAuthors, "Patterns of Enterprise Architecture");
publicBook.GenerateISBN();

books.Add(publicBook);

//create and add a special book in the list
var specialAuthors = new Author[]
{
    new Author("Sebastian", "Münster")
};

Book specialBook = new SpecialCollectionBook(specialAuthors, "Beschreibung aller Länder"); //'Cosmography', or 'A description of all countries'
specialBook.GenerateISBN(Guid.Parse("c5a03b67-2876-4e26-9ac6-84da498900ac"));

books.Add(specialBook);

//access content for an allowed user ex EGrigore, and for a random user
foreach (var book in books)
{
    Console.WriteLine($"Accessing content for: {book.Title}");

    var content = book.AccessContent("EGrigore");
    Console.WriteLine(content);
}
Console.WriteLine();

Console.WriteLine("Print ISBN of all books");
foreach (var book in books)
{
    Console.WriteLine($"{book.Title} - {book.ISBN}");
}

Console.ReadLine();

