using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Users.Infrastructure.Persistence
{
    public class DbConnectionFactory(IConfiguration config)
    {
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connectionString = config.GetConnectionString("");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Postgres connection string '' is missing.");
            }

            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
