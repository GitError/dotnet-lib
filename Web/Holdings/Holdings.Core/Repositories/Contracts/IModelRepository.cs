using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetAllAsync();

        Task<Model> GetByIdAcyns(int id);

        Task<IEnumerable<Model>> GetByPortfolioId(string userId);
    }
}