using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Holdings.Core.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PortfolioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Portfolio> Create(Portfolio portfolio)
        {
            await _unitOfWork.Portfolios.AddAsync(portfolio);
            await _unitOfWork.CommitAsync();
            return portfolio;
        }

        public async Task Update(Portfolio portfolioToBeUpdated, Portfolio portfolio)
        {
            portfolioToBeUpdated.Name = portfolio.Name;
            portfolioToBeUpdated.User = portfolio.User;
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Portfolio portfolio)
        {
            _unitOfWork.Portfolios.Remove(portfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Portfolio>> GetAll()
        {
            return await _unitOfWork.Portfolios.GetAllAsync();
        }

        public async Task<Portfolio> GetById(int id)
        {
            return await _unitOfWork.Portfolios.SingleOrDefaultAsync(x => x.PortfolioId == id); 
        }

        public async Task<IEnumerable<Portfolio>> GetByUserId(int userId)
        {
            return await _unitOfWork.Portfolios.GetByUserIdAsync(userId); 
        } 
    }
}