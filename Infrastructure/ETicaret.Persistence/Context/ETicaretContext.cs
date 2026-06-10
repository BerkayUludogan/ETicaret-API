using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ETicaret.Persistence.Context
{
    public class ETicaretContext : IdentityDbContext<AppUserEntity, AppRoleEntity, Guid>
    {
        public ETicaretContext() { }

        public ETicaretContext(DbContextOptions<ETicaretContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını sağlayan property. Update operasyonlarında Track edilen verileri yakalar.
            var datas = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.ModifiedDate= DateTime.UtcNow,
                    _ => DateTime.Now,
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
