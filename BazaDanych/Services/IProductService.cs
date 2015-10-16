using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models;

namespace Repository.Services
{
    public interface IProductService
    {
        IQueryable<Product> GetProducts();
        IQueryable<Producer> AvailableProducers();
        IQueryable<Producer> DecodeProducers(int producersNumber);

        List<int> DecodeProducerIds(int producersNumber);
        int EncodeProducers(List<Producer> producers);
        Product GetProduct(int id);

        

    }
}