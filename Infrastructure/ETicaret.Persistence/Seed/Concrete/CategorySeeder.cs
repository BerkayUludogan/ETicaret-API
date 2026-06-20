using ETicaret.Application.Common.CustomAttributes;
using ETicaret.Domain.Entities.Catalog;
using ETicaret.Persistence.Seed.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Seed.Concrete
{
    [SeedOrder(3)]
    public class CategorySeeder : ISeeder
    {
        public void Seed(DbContext context)
        {
            var electronics = SeedCategory(
                context,
                name: "Elektronik",
                slug: "elektronik",
                description: "Telefon, bilgisayar ve elektronik aksesuar kategorileri");

            SeedCategory(
                context,
                name: "Telefon",
                slug: "telefon",
                description: "Akilli telefon modelleri",
                parentCategoryId: electronics.Id);

            SeedCategory(
                context,
                name: "Bilgisayar",
                slug: "bilgisayar",
                description: "Dizustu ve masaustu bilgisayar modelleri",
                parentCategoryId: electronics.Id);

            SeedCategory(
                context,
                name: "Giyim",
                slug: "giyim",
                description: "Giyim urunleri");

            context.SaveChanges();
        }

        private static CategoryEntity SeedCategory(
            DbContext context,
            string name,
            string slug,
            string description,
            Guid? parentCategoryId = null)
        {
            var category = context.Set<CategoryEntity>()
                .FirstOrDefault(x => x.Slug == slug);

            if (category is not null)
                return category;

            category = new CategoryEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Slug = slug,
                Description = description,
                ParentCategoryId = parentCategoryId,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            context.Set<CategoryEntity>().Add(category);
            return category;
        }
    }
}
