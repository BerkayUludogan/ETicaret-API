using ETicaret.Application.Shared.Abstractions.Persistence;
using ETicaret.Domain.Entities.Common;

namespace ETicaret.Application.Shared.Abstractions.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    { 
        IReadRepository<T> GetReadRepository<T>() where T : BaseEntity,IEntityBase,new();
        IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity,IEntityBase,new();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveAsync();
    }
}
