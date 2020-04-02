using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories.Contracts
{
    public interface IHoldingRepository : IRepository<Holding>
    {
        Task<IEnumerable<Holding>> GetByModelIdAsync(int modelId);
    }
}