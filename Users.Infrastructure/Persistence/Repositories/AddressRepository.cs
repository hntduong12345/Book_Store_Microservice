using Dapper;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Infrastructure.Persistence.Repositories
{
    public class AddressRepository(DbConnectionFactory connectionFactory) : IAddressRepository
    {
        //Get functions
        public async Task<Address?> GetByIdAsync(int id)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var sql = @"
                SELECT * FROM user_addresses
                WHERE id = @Id";

            return await connection.QueryFirstOrDefaultAsync<Address>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Address>> GetByUserIdAsync(Guid userId)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var sql = @"
                SELECT * FROM user_addresses
                WHERE user_id = @UserId";

            return await connection.QueryAsync<Address>(sql, new { UserId = userId });
        }

        //Insert functions
        public async Task<int> InsertAsync(Address address)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            var sql = @"
                INSERT INTO user_addresses (user_id, address_line1, address_line2, city, state_province, postal_code, country_code, is_default, address_type)
                VALUES (@UserId, @AddressLine1, @AddressLine2, @City, @StateProvince, @PostalCode, @CountryCode, @IsDefault, @AddressType)";

            return await connection.ExecuteAsync(sql, address);
        }

        //Update functions
        public async Task<int> UpdateAsync(Address address)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            const string sql = @"
                UPDATE user_addresses 
                SET address_line1 = @AddressLine1, 
                    address_line2 = @AddressLine2, 
                    city = @City, 
                    state_province = @StateProvince, 
                    postal_code = @PostalCode, 
                    country_code = @CountryCode,
                    is_default = @IsDefault,
                    address_type = @AddressType
                WHERE id = @Id";

            return await connection.ExecuteAsync(sql, address);
        }

        public async Task<int> SetDefaultAddressAsync(Guid userId, int addressId)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();

            // Use a transaction or a single batch to ensure both happen or none happen
            var sql = @"
                UPDATE user_addresses SET is_default = false WHERE user_id = @UserId;
                UPDATE user_addresses SET is_default = true WHERE id = @AddressId AND user_id = @UserId;";

            return await connection.ExecuteAsync(sql, new { UserId = userId, AddressId = addressId });
        }

        //Delete functions
        public async Task<int> DeleteAsync(int id)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            return await connection.ExecuteAsync("DELETE FROM user_addresses WHERE id = @Id", new { Id = id });
        }
    }
}
