using System;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPortfolioRepository Portfolios { get; }

        IModelRepository Models { get; }

        IHoldingRepository Holdings { get; }

        IUserRepository Users { get; }

        Task<int> CommitAsync();
    }
}