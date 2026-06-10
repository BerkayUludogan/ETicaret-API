using ETicaret.Application.Common.Abstractions.Persistence;
using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Domain.Entities.Common;
using ETicaret.Persistence.Context;
using ETicaret.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ETicaret.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ETicaretContext _context;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(ETicaretContext context)
        {
            _context = context;
        }

        public IReadRepository<T> GetReadRepository<T>() where T : BaseEntity, IEntityBase, new()
        => new ReadRepository<T>(_context);

        public IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity, IEntityBase, new()
        => new WriteRepository<T>(_context);

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
