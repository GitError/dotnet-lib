using Holdings.Core.Models;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}