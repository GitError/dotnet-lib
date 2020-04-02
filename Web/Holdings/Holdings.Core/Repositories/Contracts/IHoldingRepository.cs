using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories
{
    public interface IHoldingRepository
    {
        Task<IEnumerable<Holding>> GetAllAsync();

        Task<Holding> GetByIdAcyns(int id);

        Task<IEnumerable<Holding>> GetByModelId(int modelId);
    }
}