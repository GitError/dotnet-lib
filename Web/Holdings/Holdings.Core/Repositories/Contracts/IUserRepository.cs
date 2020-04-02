using Holdings.Core.Models;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByNameAsync(string username);
    }
}