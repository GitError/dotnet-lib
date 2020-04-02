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
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        private HoldingsDbContext DbContext => Context as HoldingsDbContext;

        public ModelRepository(HoldingsDbContext context)
            : base(context)
        { }
        
        public async Task<IEnumerable<Model>> GetByPortfolioIdAsync(int portfolioId)
        { 
            return await DbContext.Models.Where(x => x.Portfolio.PortfolioId == portfolioId).ToListAsync();
        }
    }
}