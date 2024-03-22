namespace Application.Interfaces
{
    public interface IBooksLibraryDatabaseCreation
    {
        public Task InitiateDatabaseTablesWithData(bool shouldDeleteTables);
    }
}
