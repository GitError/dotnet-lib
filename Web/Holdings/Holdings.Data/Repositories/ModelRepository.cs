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
    public class ModelRepository : GenericRepository<Model>, IModelRepository
    {
        private HoldingsDbContext _context => _context as HoldingsDbContext;

        public ModelRepository(HoldingsDbContext context)
            : base(context)
        { }
        
        public async Task<IEnumerable<Model>> GetByPortfolioIdAsync(int portfolioId)
        { 
            return await _context.Models.Where(x => x.Portfolio.PortfolioId == portfolioId).ToListAsync();
        }
    }
}