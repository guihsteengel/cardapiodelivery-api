using FoodDelivery.Api.Models;

namespace FoodDelivery.Api.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // 🔹 RESTAURANTE
            if (!context.Restaurants.Any())
            {
                context.Restaurants.Add(new Restaurant
                {
                    Name = "Lanchonete do Stengel",
                    Address = "Rua Exemplo, 123",
                    Phone = "(00) 00000-0000"
                });
                context.SaveChanges();
            }

            var restaurantId = context.Restaurants.First().Id;

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
                        CategoryId = lanchesId,
                        RestaurantId = restaurantId
                    },
                    new Product
                    {
                        Name = "Coca-Cola Lata",
                        Description = "Refrigerante 350ml",
                        Price = 6.00m,
                        ImageUrl = "/images/coca.jpg",
                        IsAvailable = true,
                        CategoryId = bebidasId,
                        RestaurantId = restaurantId
                    },
                    new Product
                    {
                        Name = "Coca-Cola 2L",
                        Description = "Refrigerante 2 Litros",
                        Price = 10.00m,
                        ImageUrl = "/images/cocacola2l.jpg",
                        IsAvailable = true,
                        CategoryId = bebidasId,
                        RestaurantId = restaurantId
                    },
                    new Product
                    {
                        Name = "Brownie",
                        Description = "Brownie de chocolate",
                        Price = 10.00m,
                        ImageUrl = "/images/brownie.jpg",
                        IsAvailable = true,
                        CategoryId = sobremesasId,
                        RestaurantId = restaurantId
                    }
                };

                context.Products.AddRange(produtos);
                context.SaveChanges();
            }
        }
    }
}