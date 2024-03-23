using OOPPillars.BooksLibrary;

Console.WriteLine("Hello to OOP Pillars!");

BookLibrary bookLibrary = new BookLibrary();

//access content for an allowed user ex EGrigore, and for a random user
foreach (var book in bookLibrary.Books)
{
    Console.WriteLine($"Accessing content for: {book.Title}");

    var content = book.AccessContent("EGrigore");
    Console.WriteLine(content);
}
Console.WriteLine();

var martinFowlerBooks = bookLibrary.SearchBook("patterns", "Martin");
if (martinFowlerBooks.Any())
{
    Console.WriteLine("Books found for title 'patterns' and author 'Martin'");
    foreach (var book in martinFowlerBooks)
    {
        book.PrintBook();
    }
}


var notFoundBook = bookLibrary.SearchBook("design", "Khorikov");
if (notFoundBook.Count() == 0)
{
    Console.WriteLine("No books were found with title 'design' and author 'Khorikov");
}

Console.ReadLine();