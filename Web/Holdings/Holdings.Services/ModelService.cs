using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Holdings.Core.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Services
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Model> Create(Model model)
        {
            await _unitOfWork.Models.AddAsync(model);
            await _unitOfWork.CommitAsync();

            return model;
        }

        public async Task Update(Model modelToBeUpdated, Model model)
        {
            modelToBeUpdated.Name = model.Name;
            modelToBeUpdated.Description = model.Description;
            modelToBeUpdated.PortfolioId = model.PortfolioId;

            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Model model)
        {
            _unitOfWork.Models.Delete(model);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Model>> GetAll()
        {
            return await _unitOfWork.Models.GetAllAsync();
        }

        public async Task<Model> GetById(int id)
        {
            return await _unitOfWork.Models.SingleOrDefaultAsync(x => x.ModelId == id);
        }

        public async Task<IEnumerable<Model>> GetByPortfolioId(int portfolioId)
        {
            return await _unitOfWork.Models.GetByPortfolioIdAsync(portfolioId);
        }
    }
}