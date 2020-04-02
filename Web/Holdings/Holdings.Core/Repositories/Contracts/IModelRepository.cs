using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories.Contracts
{
    public interface IModelRepository : IRepository<Model>
    {
        Task<IEnumerable<Model>> GetByPortfolioIdAsync(int portfolioId);
    }
}