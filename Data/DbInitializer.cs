using FoodDelivery.Api.Models;

namespace FoodDelivery.Api.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // 🔹 CATEGORIAS
            if (!context.Categories.Any())
            {
                var categorias = new List<Category>
                {
                    new Category { Name = "Lanches" },
                    new Category { Name = "Bebidas" },
                    new Category { Name = "Sobremesas" }
                };

                context.Categories.AddRange(categorias);
                context.SaveChanges();
            }

            // 🔹 PRODUTOS
            if (!context.Products.Any())
            {
                var lanchesId = context.Categories.First(c => c.Name == "Lanches").Id;
                var bebidasId = context.Categories.First(c => c.Name == "Bebidas").Id;
                var sobremesasId = context.Categories.First(c => c.Name == "Sobremesas").Id;

                var produtos = new List<Product>
                {
                    new Product
                    {
                        Name = "X-Burger",
                        Description = "Hambúrguer artesanal com queijo",
                        Price = 25.90m,
                        ImageUrl = "/images/burger.jpg",
                        IsAvailable = true,
                        CategoryId = lanchesId
                    },
                    new Product
                    {
                        Name = "Coca-Cola Lata",
                        Description = "Refrigerante 350ml",
                        Price = 6.00m,
                        ImageUrl = "/images/coca.jpg",
                        IsAvailable = true,
                        CategoryId = bebidasId
                    },
                    new Product
                    {
                        Name = "Coca-Cola 2L",
                        Description = "Refrigerante 2 Litros",
                        Price = 10.00m,
                        ImageUrl = "/images/cocacola2l.jpg",
                        IsAvailable = true,
                        CategoryId = bebidasId
                    },
                    new Product
                    {
                        Name = "Brownie",
                        Description = "Brownie de chocolate",
                        Price = 10.00m,
                        ImageUrl = "/images/brownie.jpg",
                        IsAvailable = true,
                        CategoryId = sobremesasId
                    }
                };

                context.Products.AddRange(produtos);
                context.SaveChanges();
            }
        }
    }
}