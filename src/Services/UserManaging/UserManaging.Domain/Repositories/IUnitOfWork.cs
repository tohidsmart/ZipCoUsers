using System;
using System.Threading;
using System.Threading.Tasks;

namespace UserManaging.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
