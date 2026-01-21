using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace Users.Infrastructure.Persistence.Repositories
{
    public class UserRepository(DbConnectionFactory connectionFactory) : IUserRepository
    {
        //Get functions
        public async Task<User?> GetByEmailAsync(string email, bool includeAddresses)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            //Map entity attribute have underscore in db with the entity in class
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            //Get user's data not include addresses
            if (!includeAddresses)
            {
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM users WHERE LOWER(email) = LOWER(@Email) AND is_archived = false",
                    new { Email = email }
                    );
            }

            //Get user's data include addresses
            var sql = @"
                SELECT u.*, a.* FROM users u
                LEFT JOIN user_addresses a ON u.id = a.user_id
                WHERE LOWER(u.email) = LOWER(@Email) AND u.is_archived = false";

            var userDict = new Dictionary<Guid, User>();
            await connection.QueryAsync<User, Address, User>(sql, (user, address) =>
            {
                if (!userDict.TryGetValue(user.Id, out var existingUser))
                {
                    existingUser = user;
                    userDict.Add(existingUser.Id, existingUser);
                }

                if (address is not null)
                    existingUser.Addresses.Add(address);
                return existingUser;
            }, new { Email = email });

            return userDict.Values.FirstOrDefault();
        }

        public async Task<User?> GetActiveByIdAsync(Guid id, bool includeAddresses)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            //Map entity attribute have underscore in db with the entity in class
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            //Get user's data not include addresses
            if (!includeAddresses)
            {
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM users WHERE id = @Id AND is_archived = false",
                    new { Id = id }
                    );
            }

            //Get user's data include addresses
            var sql = @"
                SELECT u.*, a.* FROM users u
                LEFT JOIN user_addresses a ON u.id = a.user_id
                WHERE u.id = @Id AND u.is_archived = false";

            var userDict = new Dictionary<Guid, User>();
            await connection.QueryAsync<User, Address, User>(sql, (user, address) =>
            {
                if (!userDict.TryGetValue(user.Id, out var existingUser))
                {
                    existingUser = user;
                    userDict.Add(existingUser.Id, existingUser);
                }

                if (address is not null)
                    existingUser.Addresses.Add(address);
                return existingUser;
            }, new { Id = id });

            return userDict.Values.FirstOrDefault();
        }

        //For Update and Admin/Manager usage
        public async Task<User?> GetByIdAsync(Guid id)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            //Map entity attribute have underscore in db with the entity in class
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM users WHERE id = @Id",
                new { Id = id }
                );
        }

        //Insert functions
        public async Task<int> InsertAsync(User user)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            var sql = @"
                INSERT INTO users (id, email, first_name, last_name, is_active)
                VALUES (@Id, @Email, @FirstName, @LastName, @IsActive)";

            return await connection.ExecuteAsync(sql, user);
        }

        //Update functions
        public async Task<int> UpdateAsync(User user)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            var sql = @"
                UPDATE users 
                SET email = @Email, 
                    first_name = @FirstName,
                    last_name = @LastName,
                    is_active = @IsActive,
                    is_archived = @IsArchived
                WHERE id = @Id";

            return await connection.ExecuteAsync(sql, user);
        }

        //Soft delete functions
        public async Task<int> ArchivedAsync(Guid id)
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            var sql = @"
                UPDATE users 
                SET is_archived = true, updated_at = NOW()
                WHERE id = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
