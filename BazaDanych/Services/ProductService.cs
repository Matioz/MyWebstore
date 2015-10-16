using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models;
using Repository.Contexts;

namespace Repository.Services
{
    public class ProductService : IProductService
    {
        private IStoreDB _db;

        public ProductService(IStoreDB db){
            _db=db;
        }

        public IQueryable<Producer> AvailableProducers()
        {
            var producers = _db.Producers;
            return producers;
        }

        public IQueryable<Producer> DecodeProducers(int producersNumber)
        {
            var allProducers = _db.Producers.OrderBy(p=>p.ProducerId);
            IQueryable<Producer> producers = null;
            for (int i = 0; (1 << (i)) <= producersNumber; i++)
            {
                if ((producersNumber & (1 << i)) > 0)
                {
                    if (producers == null)
                    {
                        if(i>0)
                            producers = allProducers.Skip(i).Take(1);
                        else
                        {
                            producers = allProducers.Take(1);
                        }
                    }
                    else
                    {
                        producers = producers.Union(allProducers.Skip(i).Take(1));
                    }
                }
            }
            return producers;
        }

        public List<int> DecodeProducerIds(int producersNumber)
        {
            var producers = DecodeProducers(producersNumber);
            List<int> ids = new List<int>();
            foreach (var producer in producers)
            {
                ids.Add(producer.ProducerId);
            }
            return ids;
        }
        public int EncodeProducers(List<Producer> producers)
        {
            int result = 0;
            var allProducers = _db.Producers.OrderBy(p=>p.ProducerId).ToList();
            for (int i = 0, iterator = 0; i < producers.Count; iterator++)
            {
                if (allProducers.ElementAt(iterator) == producers.ElementAt(i))
                {
                    i++;
                    result += (1 << (iterator + 1));
                }
            }
            return result;
        }

        public IQueryable<Category> AvailableCategories()
        {
            var categories = _db.Categories;
            return categories;
        }

        public IQueryable<Product> GetProducts()
        {
            var products = _db.Products.AsNoTracking();
            return products;
        }

        public Product GetProduct(int id)
        {
            var product = _db.Products.Find(id);
            return product;
        }

        public static Array AvailableSizes
        {
            get
            {
                return new string[] { "S", "M", "L", "XL" };
            }
        }

        public static List<string> DecodeSizes(int size)
        {
            List<string> sizes = new List<string>();
            for (int i = 0; i < AvailableSizes.Length; i++)
            {
                if ((size & (1 << i)) > 0) sizes.Add(AvailableSizes.GetValue(i).ToString());
            }
            return sizes;
        }
        public static Array AvailableColors
        {
            get
            {
                List<string> colors = new List<string>();
                foreach (var color in ColorDictionary)
                {
                    colors.Add(color.Key);
                }
                return colors.ToArray();
            }
        }

        public static Dictionary<string, string> ColorDictionary
        {
            get
            {
                return new Dictionary<string, string> { 
                    {"Red", "Czerwony"},
                    {"Black", "Czarny"},
                    {"Blue", "Niebieski"},
                    {"Green", "Zielony"},
                    {"Yellow", "Żółty"},
                    {"Gray", "Szary"}
                };
            }
        }

        public static List<string> DecodeColors(int color)
        {
            List<string> colors = new List<string>();
            for (int i = 0; i < AvailableColors.Length; i++)
            {
                if ((color & (1 << i)) > 0) colors.Add(AvailableColors.GetValue(i).ToString());
            }
            return colors;
        }
        
    }
}