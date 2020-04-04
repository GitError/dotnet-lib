using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Holdings.Core.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Services
{
    public class HoldingService : IHoldingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HoldingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Holding> Create(Holding holding)
        {
            await _unitOfWork.Holdings.AddAsync(holding);
            await _unitOfWork.CommitAsync();

            return holding;
        }

        public async Task Update(Holding holdingToBeUpdated, Holding holding)
        {
            holdingToBeUpdated.BuyPrice = holding.BuyPrice;
            holdingToBeUpdated.Description = holding.Description;
            holdingToBeUpdated.AssetClass = holding.AssetClass;
            holdingToBeUpdated.Quantity = holding.Quantity;
            holdingToBeUpdated.Symbol = holding.Symbol;
            holdingToBeUpdated.ModelId = holding.ModelId;

            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Holding holding)
        {
            _unitOfWork.Holdings.Delete(holding); 

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Holding>> GetAll()
        {
            return await _unitOfWork.Holdings.GetAllAsync();
        }

        public async Task<Holding> GetById(int id)
        {
            return await _unitOfWork.Holdings.SingleOrDefaultAsync(x => x.ModelId == id);
        }

        public async Task<IEnumerable<Holding>> GetByModelId(int portfolioId)
        {
            return await _unitOfWork.Holdings.GetByModelIdAsync(portfolioId);
        }
    }
}