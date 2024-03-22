﻿using System.Data;
using Dapper;

using Application.Interfaces;
using Domain;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly IBooksDatabaseTransactionalContext _booksDatabaseContext;

        public BookRepository(IBooksDatabaseTransactionalContext booksDatabaseContext)
        {
            _booksDatabaseContext = booksDatabaseContext;
        }

        public async Task AddBook(Book book)
        {
            var query = "INSERT INTO Books (Id, PublicationDate, Title) VALUES (@Id, @PublicationDate, @Title)";
            var parameters = new DynamicParameters();
            parameters.Add("Id", book.Id, DbType.Guid);
            parameters.Add("PublicationDate", book.PublicationDate, DbType.Date);
            parameters.Add("Title", book.Title, DbType.String);

            var connection = _booksDatabaseContext.GetDbConnection();
            await connection.ExecuteAsync(query, parameters, _booksDatabaseContext.GetDbTransaction());
        }
    }
}
