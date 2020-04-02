using Holdings.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Core.Services
{
    public interface IModelService
    {
        Task<IEnumerable<Model>> GetAll();

        Task<Model> GetById(int id);

        Task<IEnumerable<Model>> GetByPortfolioId(int portfolioId);

        Task<Model> Create(Model Model);

        Task Update(Model modelToBeUpdated, Model model);

        Task Delete(Model model);
    }
}