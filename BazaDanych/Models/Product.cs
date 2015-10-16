using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int ProducerId { get; set; }

        public int CategoryId { get; set; }

        [Display(Name="Nazwa")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name="Cena")]
        [Required]
        [Range(0.01, 100.00)]
        public decimal Price { get; set; }

        [Display(Name = "Dostępne kolory")]
        public int Colors { get; set; }

        [Display(Name = "Dostępne rozmiary")]
        public int Sizes { get; set; }
        
        [Display(Name= "Ilość na stanie")]
        public int Amount { get; set; }

        [Required]
        [Display(Name="Kategoria")]
        public virtual Category Category { get; set; }

        public virtual Producer Producer { get; set; }

        public virtual List<Picture> Pictures { get; set; }
    }
}