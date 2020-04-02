using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> GetByUsername(string username);
         
        Task<User> Create(User user);

        Task Update(User userToBeUpdated, User user);

        Task Delete(User user);
    }
}