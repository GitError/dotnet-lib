using Holdings.Core.Models;
using Holdings.Core.Repositories;
using Holdings.Data;
using Holdings.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolios.Data.Repositories
{
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        private HoldingsDbContext DbContext => Context as HoldingsDbContext;

        public ModelRepository(HoldingsDbContext context)
            : base(context)
        { }

        public Task<Model> GetByIdAcyns(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Model>> GetByPortfolioId(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}