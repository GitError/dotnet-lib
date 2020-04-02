using System;
using System.Threading.Tasks;

namespace Holdings.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPortfolioRepository Portfolios { get; }

        IModelRepository Models { get; }

        IHoldingRepository Holdings { get; }

        Task<int> CommitAsync();
    }
}