namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Repository.Models;
    using Repository.Contexts;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Contexts.StoreDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repository.Contexts.StoreDB context)
        {
            SeedCategories(context);
            SeedProducers(context);
            SeedProducts(context);
            SeedPictures(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private void SeedCategories(StoreDB context)
        {
            for (int i = 1; i <= 5; i++)
            {
                var category = new Category()
                {
                    Name = "Kategoria " + i.ToString()
                };
                context.Set<Category>().AddOrUpdate(category);
            }
            context.SaveChanges();
        }
        private void SeedProducers(StoreDB context)
        {
            for (int i = 1; i <= 3; i++)
            {
                var producer = new Producer()
                {
                    ProducerId = i,
                    Name = "Producent " + i.ToString(),
                    FirstName = "Tom",
                    LastName = "Drugi"
                };
                context.Set<Producer>().AddOrUpdate(producer);
            }
            context.SaveChanges();
        }
        private void SeedProducts(StoreDB context)
        {
            int sizes = Repository.Services.ProductService.AvailableSizes.Length;
            int colors = Repository.Services.ProductService.AvailableColors.Length;
            for (int i = 1; i <= 60; i++)
            {
                var product = new Product()
                {
                    Category = context.Categories.Find(i%5 +1),
                    ProducerId = (i - 1) / 20 + 1,
                    Colors = i % ((1 << colors) -1 ) +1,
                    Sizes = i % ((1 << sizes)- 1) + 1,
                    Name = "Produkt " + i.ToString(),
                    Price = i / 10M,
                    Amount = (i < 5 ? 0 : i)
                };
                context.Set<Product>().AddOrUpdate(product);
            }
            context.SaveChanges(); 

        }
        private void SeedPictures(StoreDB context)
        {
            for (int i = 0; i < 20; i++)
            {
                context.Set<Picture>().AddOrUpdate(new Picture { Name = "pict1.jpg", ProductId = 1 + 3 * i });
                context.Set<Picture>().AddOrUpdate(new Picture { Name = "pict2.jpg", ProductId = 2 + 3 * i });
                context.Set<Picture>().AddOrUpdate(new Picture { Name = "pict3.jpg", ProductId = 3 + 3 * i });
            }
            context.SaveChanges();
        }
    }
}
