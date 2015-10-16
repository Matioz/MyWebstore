using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Picture
    {
        public virtual int PictureId { get; set; }

        public virtual int ProductId { get; set; }

        [StringLength(255)]
        public virtual string Name { get; set; }

        public virtual Product Product { get; set; }
    }
}