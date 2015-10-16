using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models;
using Repository.Contexts;

namespace Repository.Services
{
    public class ProducerService : IProducerService
    {
        private IStoreDB _db;

        public ProducerService(IStoreDB db){
            _db=db;
        }

        public IQueryable<Producer> GetProducers()
        {
            var producers = _db.Producers.AsNoTracking();
            return producers;
        }
    }
}