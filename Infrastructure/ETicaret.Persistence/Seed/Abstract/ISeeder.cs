using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Seed.Abstract
{
    public interface ISeeder
    {
        void Seed(DbContext context);
    }
}
