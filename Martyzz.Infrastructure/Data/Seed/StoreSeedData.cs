using Martyzz.Domain.Models;

namespace Martyzz.Infrastructure.Data.Seed
{
    public static class StoreSeedData
    {
        //public static readonly Product[] Products = BuildProducts();

        //public static void Apply(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Brand>().HasData(Brands);
        //    modelBuilder.Entity<ProductCategory>().HasData(Categories);
        //    modelBuilder.Entity<Product>().HasData(Products);
        //}

        public static void SeedAsync(StoreDbContext context)
        {
            File.ReadAllTextAsync(
                    "C:\\Users\\george\\dotnetLearning\\Martyzz\\Martyzz.Infrastructure\\Data\\Seed\\brands (1).json"
                )
                .ContinueWith(brandsTask =>
                {
                    var brands = System.Text.Json.JsonSerializer.Deserialize<Brand[]>(
                        brandsTask.Result
                    );
                    if (brands != null)
                    {
                        context.Brands.AddRange(brands);
                    }
                })
                .Wait();

            File.ReadAllTextAsync(
                    "C:\\Users\\george\\dotnetLearning\\Martyzz\\Martyzz.Infrastructure\\Data\\Seed\\categories (1).json"
                )
                .ContinueWith(categoriesTask =>
                {
                    var categories = System.Text.Json.JsonSerializer.Deserialize<Category[]>(
                        categoriesTask.Result
                    );
                    context.Categories.AddRange(categories);
                })
                .Wait();

            File.ReadAllTextAsync(
                    "C:\\Users\\george\\dotnetLearning\\Martyzz\\Martyzz.Infrastructure\\Data\\Seed\\products (1).json"
                )
                .ContinueWith(productsTask =>
                {
                    var products = System.Text.Json.JsonSerializer.Deserialize<Product[]>(
                        productsTask.Result
                    );
                    context.Products.AddRange(products);
                })
                .Wait();

            context.SaveChanges();
        }

        //private static Product[] BuildProducts()
        //{
        //    string[] sizes = ["Small", "Medium", "Large", "XL", "Double"];
        //    string[] drinks =
        //    [
        //        "Espresso",
        //        "Americano",
        //        "Latte",
        //        "Iced Latte",
        //        "Cappuccino",
        //        "Flat White",
        //        "Mocha",
        //        "Caramel Macchiato",
        //        "Cold Brew",
        //        "Iced Americano",
        //        "Vanilla Latte",
        //        "Hazelnut Latte",
        //        "White Mocha",
        //        "Matcha Latte",
        //        "Chai Latte",
        //        "Affogato",
        //        "Spanish Latte",
        //        "Iced Mocha",
        //        "Nitro Cold Brew",
        //        "Filter Coffee",
        //    ];

        //    string[] styles =
        //    [
        //        "Classic",
        //        "Extra Shot",
        //        "Sugar Free",
        //        "Caramel Twist",
        //        "Vanilla Cream",
        //        "Hazelnut Bliss",
        //        "Cinnamon Spice",
        //        "Dark Roast",
        //        "Oat Milk",
        //        "Coconut Milk",
        //    ];

        //    var products = new Product[1000];
        //    int productNumber = 0;

        //    for (int styleIndex = 0; styleIndex < styles.Length; styleIndex++)
        //    {
        //        for (int drinkIndex = 0; drinkIndex < drinks.Length; drinkIndex++)
        //        {
        //            for (int sizeIndex = 0; sizeIndex < sizes.Length; sizeIndex++)
        //            {
        //                Guid productId = Guid.Parse(
        //                    $"30000000-0000-0000-0000-{(productNumber + 1).ToString("D12")}"
        //                );
        //                string size = sizes[sizeIndex];
        //                string drink = drinks[drinkIndex];
        //                string style = styles[styleIndex];
        //                string name = $"{size} {drink} - {style}";
        //                decimal price =
        //                    2.50m
        //                    + (sizeIndex * 0.70m)
        //                    + (drinkIndex * 0.08m)
        //                    + (styleIndex * 0.12m);

        //                products[productNumber] = new Product
        //                {
        //                    Id = productId,
        //                    Name = name,
        //                    Describtion = $"{drink} prepared in {style.ToLowerInvariant()} style.",
        //                    Price = decimal.Round(price, 2),
        //                    PictureUrl = $"/images/products/{(productNumber + 1):D4}.jpg",
        //                    BrandId = BrandIds[productNumber % BrandIds.Length],
        //                    ProductCategoryId = CategoryIds[productNumber % CategoryIds.Length],
        //                };

        //                productNumber++;
        //            }
        //        }
        //    }

        //    return products;
        //}
    }
}
