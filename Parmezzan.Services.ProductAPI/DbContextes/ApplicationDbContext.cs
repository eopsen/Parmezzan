using Microsoft.EntityFrameworkCore;
using Parmezzan.Services.ProductAPI.Models;

namespace Parmezzan.Services.ProductAPI.DbContextes
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Spaghetti carbonara",
                Price = 29,
                Description = "Pasta alla carbonara – potrawa kuchni włoskiej złożona z makaronu, jajek, pancetty lub guanciale, sera pecorino romano lub parmezanu oraz czarnego pieprzu. Niepoprawnym jest mówienie o „sosie carbonara”, ponieważ makaron i pozostałe składniki stanowią tu nierozerwalną całość.",
                ImageUrl = "https://assets.tmecosys.com/image/upload/t_web767x639/img/recipe/ras/Assets/61FCB2B2-DB84-4A68-ABB8-F9FBA800BCA3/Derivates/FCB48A53-86F0-4697-BE75-7B7CE9B49EBE.jpg",
                CategoryName = "Entree"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Lasagna classica",
                Price = 35,
                Description = "Lasagne, lazania – rodzaj makaronu w postaci dużych, prostokątnych płatów, a także danie przygotowywane na bazie tego makaronu. Makaron jest uprzednio gotowany lub układany na sucho w prostokątnym naczyniu, na przemian z warstwami farszu. Następnie potrawa jest zapiekana.",
                ImageUrl = "https://d167y3o4ydtmfg.cloudfront.net/357/studio/assets/v1545738807011_736924535/Classico%20Two%20Sauce%20Lasagna-min.jpg",
                CategoryName = "Entree"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Panna Cotta",
                Price = 10.99,
                Description = "Panna Cotta oznacza gotowaną śmietankę. Do śmietany dodaje się cukier i często wanilię, a usztywnia żelatyną. Gotowy deser wykłada się do góry dnem na talerz. Żelatyny należy dodać tyle, aby deser trzymam kształt, ale nie był za sztywny.",
                ImageUrl = "https://bi.im-g.pl/im/45/06/1a/z27290437Q,Panna-Cotta.jpg",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Tiramisu",
                Price = 10,
                Description = "Tiramisu – deser złożony z warstwy biszkoptu nasączonej bardzo mocną kawą espresso, na którą nakłada się warstwę kremu z sera mascarpone, żółtek jaj kurzych i cukru, a całość posypuje się warstwą grubo zmielonych płatków gorzkiej czekolady.",
                ImageUrl = "https://staticsmaker.iplsc.com/smaker_production_2022_01_25/3880ee56bfb96556229f5da649a0a6b4-recipe_main.jpg",
                CategoryName = "Dessert"
            });
        }
    }
}
