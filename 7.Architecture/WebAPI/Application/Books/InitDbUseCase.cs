using Application.Interfaces;

namespace Application.Books
{
    public class InitDbUseCase
    {
        private readonly IBooksLibraryDatabaseCreation _booksLibraryDatabaseCreation;

        public InitDbUseCase(IBooksLibraryDatabaseCreation booksLibraryDatabaseCreation)
        {
            this._booksLibraryDatabaseCreation = booksLibraryDatabaseCreation;
        }

        public async Task InitDb(bool shouldDeleteTables)
        {
            await this._booksLibraryDatabaseCreation.InitiateDatabaseTablesWithData(shouldDeleteTables);
        }

    }
}
