using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories.Contracts
{
    public interface IPortfolioRepository : IGenericRepository<Portfolio>
    {
        Task<IEnumerable<Portfolio>> GetByUserIdAsync(int userId);

        Task<IEnumerable<Portfolio>> GetByUserameAsync(string username);
    }
}