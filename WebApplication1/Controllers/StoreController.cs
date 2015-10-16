using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using Repository.Services;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using PagedList;

namespace WebApplication1.Controllers
{
    public class StoreController : Controller
    {
        private IProductService _productService;

        public StoreController(IProductService productService)
        {
            _productService = productService;
        }

        #region index
        // GET: Product
        public ActionResult Index()
        {

            return View();
        }

        #region 26.07

        public ActionResult Search(int? page, string sortOrder, string product, List<int> producer, int? priceFrom, int? priceTo, List<int> size,
            List<int> color, bool? availableOnly, int? category)
        {
            int currentPage = page ?? 1;
            int pageSize = 10;

            var products = _productService.GetProducts();
            #region filters
            if (category != null)
            {
                products = products.Where(p => p.Category.CategoryId == category);
                ViewBag.Category = category;
            }
            if (priceFrom != null && priceTo != null && priceFrom > priceTo)
            {
                var largerPrice = priceFrom;
                priceFrom = priceTo;
                priceTo = largerPrice;
            }
            if (availableOnly != null && availableOnly == true)
            {
                products = products.Where(p => p.Amount > 0);
                ViewBag.AvailableOnly = true;
            }
            ViewBag.Product = product;
            if (product != "" && product != null)
            {
                products = products.Where(p => p.Name.Contains(product));
            }
            if (producer != null && producer.Count > 0)
            {
                int producers = 0;
                foreach (var p in producer)
                {
                    producers += p;
                }
                ViewBag.Producer = producers;
                List<int> selectedProducers = _productService.DecodeProducerIds(producers);
                products = products.Where(p => selectedProducers.Contains(p.ProducerId));
            }
            if (priceFrom != null)
            {
                ViewBag.PriceFrom = priceFrom.ToString();
                products = products.Where(p => p.Price >= priceFrom);
            }
            if (priceTo != null)
            {
                ViewBag.PriceTo = priceTo.ToString();
                products = products.Where(p => p.Price <= priceTo);
            }
            if (size != null)
            {
                int selectedSizes = 0;
                foreach (int s in size)
                {
                    if (s > 0)
                    {
                        selectedSizes += s;
                    }
                }
                products = products.Where(p => ((p.Sizes & selectedSizes) > 0 || selectedSizes == 0));
                ViewBag.Size = selectedSizes;
            }
            if (color != null)
            {
                int selectedColors = 0;
                foreach (int c in color)
                {
                    if (c > 0)
                    {
                        selectedColors += c;
                    }
                }
                products = products.Where(p => ((p.Colors & selectedColors) > 0 || selectedColors == 0));
                ViewBag.Color = selectedColors;
            }

            #endregion
            #region sort
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ProductSort = sortOrder == "NameAsc" ? "Name" : "NameAsc";
            ViewBag.ProducerSort = sortOrder == "ProducerAsc" ? "Producer" : "ProducerAsc";
            ViewBag.PriceSort = sortOrder == "PriceAsc" ? "Price" : "PriceAsc";
            if (User.IsInRole("Admin"))
            {
                ViewBag.AmountSort = sortOrder == "AmountAsc" ? "Amount" : "AmountAsc";
            }
            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "NameAsc":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "PriceAsc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "Producer":
                    products = products.OrderByDescending(p => p.Producer.Name);
                    break;
                case "ProducerAsc":
                    products = products.OrderBy(p => p.Producer.Name);
                    break;
                case "Amount":
                    if (User.IsInRole("Admin"))
                        products = products.OrderByDescending(p => p.Amount);
                    else
                        products = products.OrderBy(p => p.ProductId);
                    break;
                case "AmountAsc":
                    if (User.IsInRole("Admin"))
                        products = products.OrderBy(p => p.Amount);
                    else
                        products = products.OrderBy(p => p.ProductId);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductId);
                    break;
            }
            #endregion
            IPagedList<Product> results = products.ToPagedList(currentPage, pageSize);
            return PartialView("_Search", results);
        }
        #endregion 

        
        public ActionResult SearchResults(int? page, string sortOrder, string product, List<int> producer, int? priceFrom, int? priceTo, List<int> size,
            List<int> color, bool? availableOnly, int? category)
        {
            int currentPage = page ?? 1;
            int pageSize = 10;

            var products = _productService.GetProducts();
            #region filters
            if (category != null)
            {
                products = products.Where(p => p.Category.CategoryId == category);
                ViewBag.Category = category;
            }
            if (priceFrom != null && priceTo != null && priceFrom > priceTo)
            {
                var largerPrice = priceFrom;
                priceFrom = priceTo;
                priceTo = largerPrice;
            }
            if (availableOnly != null && availableOnly == true)
            {
                products = products.Where(p => p.Amount > 0);
                ViewBag.AvailableOnly = true;
            }
            ViewBag.Product = product;
            if (product != "" && product != null)
            {
                products = products.Where(p => p.Name.Contains(product));
            }
            if (producer != null && producer.Count > 0)
            {
                int producers = 0;
                foreach (var p in producer)
                {
                    producers += p;
                }
                ViewBag.Producer = producers;
                List<int> selectedProducers = _productService.DecodeProducerIds(producers);
                products = products.Where(p => selectedProducers.Contains(p.ProducerId));
            }
            if (priceFrom != null)
            {
                ViewBag.PriceFrom = priceFrom.ToString();
                products = products.Where(p => p.Price >= priceFrom);
            }
            if (priceTo != null)
            {
                ViewBag.PriceTo = priceTo.ToString();
                products = products.Where(p => p.Price <= priceTo);
            }
            if (size != null)
            {
                int selectedSizes = 0;
                foreach (int s in size)
                {
                    if (s > 0)
                    {
                        selectedSizes += s;
                    }
                }
                products = products.Where(p => ((p.Sizes & selectedSizes) > 0 || selectedSizes == 0));
                ViewBag.Size = selectedSizes;
            }
            if (color != null)
            {
                int selectedColors = 0;
                foreach (int c in color)
                {
                    if (c > 0)
                    {
                        selectedColors += c;
                    }
                }
                products = products.Where(p => ((p.Colors & selectedColors) > 0 || selectedColors == 0));
                ViewBag.Color = selectedColors;
            }

            #endregion
            #region sort
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ProductSort = sortOrder == "NameAsc" ? "Name" : "NameAsc";
            ViewBag.ProducerSort = sortOrder == "ProducerAsc" ? "Producer" : "ProducerAsc";
            ViewBag.PriceSort = sortOrder == "PriceAsc" ? "Price" : "PriceAsc";
            if (User.IsInRole("Admin"))
            {
                ViewBag.AmountSort = sortOrder == "AmountAsc" ? "Amount" : "AmountAsc";
            }
            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "NameAsc":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "Price":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "PriceAsc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "Producer":
                    products = products.OrderByDescending(p => p.Producer.Name);
                    break;
                case "ProducerAsc":
                    products = products.OrderBy(p => p.Producer.Name);
                    break;
                case "Amount":
                    if (User.IsInRole("Admin"))
                        products = products.OrderByDescending(p => p.Amount);
                    else
                        products = products.OrderBy(p => p.ProductId);
                    break;
                case "AmountAsc":
                    if (User.IsInRole("Admin"))
                        products = products.OrderBy(p => p.Amount);
                    else
                        products = products.OrderBy(p => p.ProductId);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductId);
                    break;
            }
            #endregion
            IPagedList<Product> results = products.ToPagedList(currentPage, pageSize);
            if (results.TotalItemCount == 0)
                return PartialView("_NoProducts");
            else
                return PartialView("_SearchResults", results);
        }
        public ActionResult ProducersForm(int? page, string sortOrder, string product, int? producer, int? priceFrom, int? priceTo, List<int> size,
            List<int> color, bool? availableOnly, int? category)
        {
            var products = _productService.GetProducts();

            #region filters
            if (category != null)
            {
                products = products.Where(p => p.Category.CategoryId == category);
                ViewBag.Category = category;
            }
            if (priceFrom != null && priceTo != null && priceFrom > priceTo)
            {
                var largerPrice = priceFrom;
                priceFrom = priceTo;
                priceTo = largerPrice;
            }
            if (availableOnly != null && availableOnly == true)
            {
                products = products.Where(p => p.Amount > 0);
                ViewBag.AvailableOnly = true;
            }
            ViewBag.Product = product;
            ViewBag.Producer = producer;
            if (product != "" && product != null)
            {
                products = products.Where(p => p.Name.Contains(product));
            }
            if (priceFrom != null)
            {
                ViewBag.PriceFrom = priceFrom.ToString();
                products = products.Where(p => p.Price >= priceFrom);
            }
            if (priceTo != null)
            {
                ViewBag.PriceTo = priceTo.ToString();
                products = products.Where(p => p.Price <= priceTo);
            }
            if (size != null)
            {
                int selectedSizes = 0;
                foreach (int s in size)
                {
                    if (s > 0)
                    {
                        selectedSizes += s;
                    }
                }
                products = products.Where(p => ((p.Sizes & selectedSizes) > 0 || selectedSizes == 0));
                ViewBag.Size = selectedSizes;
            }
            if (color != null)
            {
                int selectedColors = 0;
                foreach (int c in color)
                {
                    if (c > 0)
                    {
                        selectedColors += c;
                    }
                }
                products = products.Where(p => ((p.Colors & selectedColors) > 0 || selectedColors == 0));
                ViewBag.Color = selectedColors;
            }

            #endregion
            var producers = _productService.AvailableProducers().OrderBy(p => p.ProducerId).ToList();
            Dictionary<Producer, int> model = new Dictionary<Producer, int>();
            for (int i = 0; i < producers.Count; i++)
            {
                int Id = producers.ElementAt(i).ProducerId;
                model.Add(producers.ElementAt(i), products.Count(p => p.ProducerId == Id));
            }
            ViewBag.Producer = producer;
            return PartialView("_ProducersForm", model);
        }

        [ChildActionOnly]
        public ActionResult SearchForm(string product, List<int> producer, int? priceFrom, int? priceTo, List<int> size,
            List<int> color, bool? availableOnly, int? category)
        {
            ViewBag.Category = category;
            if (priceFrom != null && priceTo != null && priceFrom > priceTo)
            {
                var largerPrice = priceFrom;
                priceFrom = priceTo;
                priceTo = largerPrice;
            }
            ViewBag.Product = product;
            if (producer != null && producer.Count > 0)
            {
                int producers = 0;
                foreach (var p in producer)
                {
                    producers += p;
                }
                ViewBag.Producer = producers;
            }
            ViewBag.AvailableOnly = availableOnly;
            if (priceFrom != null)
            {
                ViewBag.PriceFrom = priceFrom.ToString();
            }
            if (priceTo != null)
            {
                ViewBag.PriceTo = priceTo.ToString();
            }
            if (size != null)
            {
                ViewBag.Size = 0;
                foreach (int s in size)
                {
                    ViewBag.Size += s;
                }
            }
            if (color != null)
            {
                ViewBag.Color = 0;
                foreach (int c in color)
                {
                    ViewBag.Color += c;
                }
            }
            return PartialView("_SearchForm");
        }

        public ActionResult QuickSearch(string term, string wanted)
        {

            if (wanted == "products")
            {
                var products = _productService.GetProducts().Where(p => p.Name.Contains(term)).
                    Take(3).ToList().Select(p => new { value = p.Name });
                return Json(products, JsonRequestBehavior.AllowGet);
            }
            if (wanted == "producers")
            {
                var producers = _productService.AvailableProducers().Where(p => p.Name.Contains(term)).
                Take(3).ToList().Select(p => new { value = p.Name });
                return Json(producers, JsonRequestBehavior.AllowGet);

            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productService.GetProduct((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //// GET: Product/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Size = new List<string>() { "S", "M", "L" };
        //    ViewBag.ProducerId = new SelectList(_productService.AvailableProducers().AsEnumerable(), "ProducerId", "ProducerName");
        //    return View();
        //}

        //// POST: Product/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ProductId,ProducerId,ProductName,Price,Size")] Product product, HttpPostedFileBase upload)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (upload != null && upload.ContentLength > 0)
        //            {


        //                var Picture = new Picture
        //                {
        //                    PictureName = System.IO.Path.GetFileName(upload.FileName),
        //                    ContentType = upload.ContentType
        //                };
        //                using (var reader = new System.IO.BinaryReader(upload.InputStream))
        //                {
        //                    Picture.Content = reader.ReadBytes(upload.ContentLength);
        //                }

        //                //Bitmap original = new Bitmap(upload.InputStream);

        //                product.Picture = new List<Picture> { Picture };
        //            }
        //            db.Products.Add(product);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", product.ProducerId);
        //        return View(product);
        //    }
        //    catch (RetryLimitExceededException /* dex */)
        //    {
        //        //Log the error (uncomment dex variable name and add a line here to write a log.
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //    }
        //    return View(product);
        //}

        //// GET: Product/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", product.ProducerId);
        //    return View(product);
        //}

        //// POST: Product/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProducerId,ProductName,Price,Size")] Product product, int id)
        //{
        //    //Zabezpiecznienie przed wejsciem spoza serwera
        //    if (HttpContext.Request.Url != HttpContext.Request.UrlReferrer) return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
        //    if (ModelState.IsValid)
        //    {
        //        product.ProductId = id;
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", product.ProducerId);
        //    return View(product);
        //}

        //// GET: Product/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// POST: Product/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Product product = db.Products.Find(id);
        //    db.Products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
