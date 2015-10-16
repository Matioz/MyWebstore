using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models;
using Repository.Contexts;

namespace Repository.Services
{
    public class PictureService : IPictureService
    {
        private IStoreDB _db;
        public PictureService(IStoreDB db)
        {
            _db = db;
        }

        public void SavePicture(Product product, HttpPostedFile pictureFile)
        {
            Picture picture = new Picture()
            {
                Name = new Guid().ToString() + pictureFile.FileName.ToString(),
                ProductId = product.ProductId
            };
            product.Pictures.Add(picture);
            pictureFile.SaveAs("~/Images/" + picture.Name);
        }

    }
}