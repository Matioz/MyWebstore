using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Repository.Models;

namespace Repository.Contexts
{
    public class StoreDB: DbContext, IStoreDB
    {
        public StoreDB(): base("name=StoreDB")
        {
        }
        public static StoreDB Create()
        {
            return new StoreDB();
        }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
    public class StoreDBInitializer : CreateDatabaseIfNotExists<StoreDB>
    {
    }
}