using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Services.Contracts
{
    public interface IPortfolioService
    {
        Task<IEnumerable<Portfolio>> GetAll();

        Task<Portfolio> GetById(int id);

        Task<IEnumerable<Portfolio>> GetByUserId(int userId);

        Task<Portfolio> Create(Portfolio portfolio);

        Task Update(Portfolio portfolioToBeUpdated, Portfolio portfolio);

        Task Delete(Portfolio portfolio);
    }
}