using Holdings.Core.Repositories.Contracts;
using Holdings.Data.Repositories;
using Portfolios.Data.Repositories;
using System.Threading.Tasks;

namespace Holdings.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HoldingsDbContext _dbContext;

        private IPortfolioRepository portfolioRepository;

        private IModelRepository modelRepository;

        private IHoldingRepository holdingRepository;

        private IUserRepository userRepository;

        public UnitOfWork(HoldingsDbContext context)
        {
            _dbContext = context;
        }

        public IPortfolioRepository Portfolios => portfolioRepository = portfolioRepository ?? new PortfolioRepository(_dbContext);

        public IModelRepository Models => modelRepository = modelRepository ?? new ModelRepository(_dbContext);

        public IHoldingRepository Holdings => holdingRepository = holdingRepository ?? new HoldingRepository(_dbContext);

        public IUserRepository Users => userRepository = userRepository ?? new UserRepository(_dbContext);

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}