using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Producer
    {

        [Display(Name="Id:")]
        public virtual int ProducerId { get; set; }

        [Display(Name="Nazwa Producenta")]
        public virtual string Name { get; set; }

        [Display(Name="Imie:")]
        public virtual string FirstName { get; set; }

        [Display(Name="Nazwisko:")]
        public virtual string LastName { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}