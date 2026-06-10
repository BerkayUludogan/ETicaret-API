
using ETicaret.Application.Common.Abstractions.Persistence;
using ETicaret.Domain.Entities.Common;

namespace ETicaret.Application.Common.Abstractions.UnitOfWorks
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
