using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Category
    {
        public virtual int CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public virtual string Name { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}