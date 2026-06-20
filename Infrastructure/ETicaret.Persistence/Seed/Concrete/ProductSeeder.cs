using ETicaret.Application.Common.CustomAttributes;
using ETicaret.Domain.Entities.Catalog;
using ETicaret.Persistence.Seed.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Seed.Concrete
{
    [SeedOrder(4)]
    public class ProductSeeder : ISeeder
    {
        public void Seed(DbContext context)
        {
            var phoneCategoryId = GetCategoryId(context, "telefon");
            var computerCategoryId = GetCategoryId(context, "bilgisayar");
            var clothingCategoryId = GetCategoryId(context, "giyim");

            if (phoneCategoryId is not null)
            {
                SeedProduct(
                    context,
                    name: "iPhone 15 128 GB",
                    slug: "iphone-15-128-gb",
                    description: "128 GB depolama alanina sahip siyah iPhone 15 modeli",
                    price: 52999.99m,
                    discountPrice: 48999.99m,
                    stockQuantity: 20,
                    sku: "IPHONE-15-128-BLACK",
                    isFeatured: true,
                    categoryId: phoneCategoryId.Value);

                SeedProduct(
                    context,
                    name: "Samsung Galaxy S24 256 GB",
                    slug: "samsung-galaxy-s24-256-gb",
                    description: "256 GB depolama alanina sahip Samsung Galaxy S24 modeli",
                    price: 45999.99m,
                    discountPrice: 42999.99m,
                    stockQuantity: 15,
                    sku: "SAMSUNG-S24-256-GRAY",
                    isFeatured: true,
                    categoryId: phoneCategoryId.Value);
            }

            if (computerCategoryId is not null)
            {
                SeedProduct(
                    context,
                    name: "MacBook Air M2 13",
                    slug: "macbook-air-m2-13",
                    description: "Apple M2 islemcili 13 inc MacBook Air modeli",
                    price: 58999.99m,
                    discountPrice: null,
                    stockQuantity: 8,
                    sku: "MACBOOK-AIR-M2-13",
                    isFeatured: true,
                    categoryId: computerCategoryId.Value);

                SeedProduct(
                    context,
                    name: "Lenovo IdeaPad 15",
                    slug: "lenovo-ideapad-15",
                    description: "Gunluk kullanim icin 15 inc Lenovo IdeaPad dizustu bilgisayar",
                    price: 24999.99m,
                    discountPrice: 21999.99m,
                    stockQuantity: 12,
                    sku: "LENOVO-IDEAPAD-15",
                    isFeatured: false,
                    categoryId: computerCategoryId.Value);
            }

            if (clothingCategoryId is not null)
            {
                SeedProduct(
                    context,
                    name: "Erkek Basic T-Shirt",
                    slug: "erkek-basic-t-shirt",
                    description: "Pamuklu basic erkek t-shirt",
                    price: 499.99m,
                    discountPrice: 349.99m,
                    stockQuantity: 50,
                    sku: "TSHIRT-BASIC-MEN-BLACK",
                    isFeatured: false,
                    categoryId: clothingCategoryId.Value);
            }

            context.SaveChanges();
        }

        private static Guid? GetCategoryId(DbContext context, string slug)
        {
            return context.Set<CategoryEntity>()
                .Where(x => x.Slug == slug && !x.IsDeleted)
                .Select(x => (Guid?)x.Id)
                .FirstOrDefault();
        }

        private static void SeedProduct(
            DbContext context,
            string name,
            string slug,
            string description,
            decimal price,
            decimal? discountPrice,
            int stockQuantity,
            string sku,
            bool isFeatured,
            Guid categoryId)
        {
            var productExists = context.Set<ProductEntity>()
                .Any(x => x.Slug == slug || x.SKU == sku);

            if (productExists)
                return;

            var product = new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Slug = slug,
                Description = description,
                Price = price,
                DiscountPrice = discountPrice,
                StockQuantity = stockQuantity,
                SKU = sku,
                IsActive = true,
                IsFeatured = isFeatured,
                CategoryId = categoryId,
                CreatedDate = DateTime.UtcNow
            };

            context.Set<ProductEntity>().Add(product);
        }
    }
}
