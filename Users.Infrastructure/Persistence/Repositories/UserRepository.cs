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
        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        //Insert functions
        public Task InsertAsync(User user)
        {
            throw new NotImplementedException();
        }

        //Update functions
        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        //Soft delete functions
        public Task ArchivedAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
