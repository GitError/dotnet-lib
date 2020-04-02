using Holdings.Core.Models;
using Holdings.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Data.Repositories
{
    public class HoldingRepository : Repository<Holding>, IHoldingRepository
    {
        private HoldingsDbContext DbContext => Context as HoldingsDbContext;

        public HoldingRepository(HoldingsDbContext context)
            : base(context)
        { }

        public new async Task<IEnumerable<Holding>> GetAllAsync()
        {
            return await DbContext.Holdings.ToListAsync();
        }

        public async Task<Holding> GetByIdAcyns(int id)
        {
            return await DbContext.Holdings.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<IEnumerable<Holding>> GetByModelId(int modelId)
        {
            throw new NotImplementedException();
        }
    }
}