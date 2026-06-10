using ETicaret.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ETicaret.Application.Shared.Abstractions.Persistence
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity, IEntityBase, new()
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetAllByPaging(bool tracking = true, int currentPage = 1, int pageSize = 5);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T?> GetByIdAsync(Guid Id, bool tracking = true);
        Task<int> CountAsync(Expression<Func<T, bool>>? method = null);
    }
}
