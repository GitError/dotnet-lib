using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Services.Contracts
{
    public interface IHoldingService
    {
        Task<IEnumerable<Holding>> GetAll();

        Task<Holding> GetById(int id);

        Task<IEnumerable<Holding>> GetByModelId(int portfolioId);

        Task<Holding> Create(Holding holding);

        Task Update(Holding holdingToBeUpdated, Holding holding);

        Task Delete(Holding holding);
    }
}