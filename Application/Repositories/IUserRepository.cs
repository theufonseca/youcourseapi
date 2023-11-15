using Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<int> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(int id);
        Task<User?> Get(int id);
        Task<User?> Get(string email);
        Task<IEnumerable<User>> GetAll();
    }
}
