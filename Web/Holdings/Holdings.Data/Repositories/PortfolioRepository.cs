using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Holdings.Data;
using Holdings.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolios.Data.Repositories
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        private HoldingsDbContext DbContext => Context as HoldingsDbContext;

        public PortfolioRepository(HoldingsDbContext context)
            : base(context)
        { }
        
        public async Task<IEnumerable<Portfolio>> GetByUserIdAsync(int userId)
        {
            return await DbContext.Portfolios.Where(x => x.User.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Portfolio>> GetByUserameAsync(string username)
        {
            return await DbContext.Portfolios.Where(x => x.User.Username == username).ToListAsync();
        }
    }
}