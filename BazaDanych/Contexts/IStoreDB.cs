using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models;
using System.Data.Entity;

namespace Repository.Contexts
{
    public interface IStoreDB
    {
        DbSet<Producer> Producers { get; set; }
        DbSet<Picture> Pictures { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        int SaveChanges();
    }
}