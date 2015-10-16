using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.Services
{
    public interface IProducerService
    {
        IQueryable<Producer> GetProducers();
    }
}
