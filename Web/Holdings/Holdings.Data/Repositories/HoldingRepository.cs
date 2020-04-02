using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holdings.Data.Repositories
{
    public class HoldingRepository : Repository<Holding>, IHoldingRepository
    {
        private HoldingsDbContext DbContext => Context as HoldingsDbContext;

        public HoldingRepository(HoldingsDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Holding>> GetByModelIdAsync(int modelId)
        {
            return await DbContext.Holdings.Where(x => x.Model.ModelId == modelId).ToListAsync();
        }
    }
}