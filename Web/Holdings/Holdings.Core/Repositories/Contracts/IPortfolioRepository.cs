using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Portfolio>> GetAllAsync();

        Task<Portfolio> GetByIdAcyns(int id);

        Task<IEnumerable<Portfolio>> GetByUserId(string userId);
    }
}