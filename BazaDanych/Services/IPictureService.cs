using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;
using System.Web;

namespace Repository.Services
{
    public interface IPictureService
    {
        void SavePicture(Product product, HttpPostedFile pictureFile);
    }
}
