using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using Infrastructure.Interfaces;

namespace Infrastructure.Persistence
{
    public class BooksDatabaseTransactionalContext: IBooksDatabaseTransactionalContext
    {
        private readonly string _databaseConnectionString;
        private SqlConnection _databaseConnection;
        private IDbTransaction? _databaseTransaction;

        public BooksDatabaseTransactionalContext(IConfiguration configuration)
        {
            //SqlMapper.AddTypeMap(typeof(DateOnly), (DbType) - 1, true);

            var configurationConnectionString = configuration.GetConnectionString("BooksLibraryDatabaseConnection");
            _databaseConnectionString = configurationConnectionString ??
                                        throw new ArgumentNullException(
                                            "BooksLibraryDatabaseConnection is not provided in the configuration file.",
                                            nameof(configurationConnectionString));

            _databaseConnection = new SqlConnection(_databaseConnectionString);
        }

        public SqlConnection DatabaseConnection => _databaseConnection;

        public SqlConnection GetDbConnection()
        {
            return this._databaseConnection;
        }

        public IDbTransaction DatabaseTransaction => _databaseTransaction;
        public IDbTransaction GetDbTransaction()
        {
            return this._databaseTransaction;
        }

        public async Task OpenDatabaseConnection()
        {
            if (_databaseConnection.State != ConnectionState.Open)
            {
                await _databaseConnection.OpenAsync();
            }
        }

        public async Task CreateDatabaseTransaction()
        {
            await OpenDatabaseConnection();
            _databaseTransaction = _databaseConnection.BeginTransaction();
        }

        public void CommitDatabaseTransaction()
        {
            if (_databaseTransaction == null)
            {
                throw new InvalidOperationException("Database transaction was not opened.");
            }

            _databaseTransaction.Commit();
        }

        public void RollbackDatabaseTransaction()
        {
            if (_databaseTransaction == null)
            {
                throw new InvalidOperationException("Database transaction was not opened.");
            }

            _databaseTransaction.Rollback();
        }

        public void Dispose()
        {
            _databaseConnection.Dispose();
            _databaseTransaction?.Dispose();
        }
    }
}
