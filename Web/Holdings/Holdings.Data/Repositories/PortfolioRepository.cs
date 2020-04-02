using Holdings.Core.Models;
using Holdings.Core.Repositories;
using Holdings.Data;
using Holdings.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolios.Data.Repositories
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        private HoldingsDbContext DbContext => Context as HoldingsDbContext;

        public PortfolioRepository(HoldingsDbContext context)
            : base(context)
        { }

        public Task<Portfolio> GetByIdAcyns(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Portfolio>> GetByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}