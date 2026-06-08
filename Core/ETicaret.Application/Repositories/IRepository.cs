using ETicaret.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;


namespace ETicaret.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity, IEntityBase, new()
    {
        DbSet<T> Table { get; }
    }
}
