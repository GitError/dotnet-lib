using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holdings.Data.Repositories
{
    public class HoldingRepository : GenericRepository<Holding>, IHoldingRepository
    {
        private HoldingsDbContext _context => _context as HoldingsDbContext;

        public HoldingRepository(HoldingsDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Holding>> GetByModelIdAsync(int modelId)
        {
            return await _context.Holdings.Where(x => x.Model.ModelId == modelId).ToListAsync();
        }
    }
}