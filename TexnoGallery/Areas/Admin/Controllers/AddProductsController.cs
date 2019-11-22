using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Cors;
using System.Web.Mvc;
using TexnoGallery.Models;
using TexnoGallery.ViewModel.Default;

namespace TexnoGallery.Areas.Admin.Controllers
{
    //[AuthenticationFilter]
    public class AddProductsController : Controller
    {
        private TexnoGalleryEntities db; 
        public AddProductsController()
        {
            db = new TexnoGalleryEntities(); ;
        }
        // GET: Admin/AddProducts
        public ActionResult Index(int Page=1)
        {
            //var product = db.Products.Include(p => p.SubCategory);

            var productList = db.Products.Include(p => p.SubCategory).OrderByDescending(pr => pr.Id).ToPagedList(Page, 10);
            ViewBag.CategName = db.Categories.ToList();
            return View(productList);
        }

        // GET: Admin/AddProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]

        public ActionResult GetCategory(int? Id)
        {
            object data = null;
            if (Id == null)
            {
                data = new
                {
                    status = 404,
                    message = "Id not found!",
                    response = ""
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            Category selectCategory = db.Categories.FirstOrDefault(ct => ct.Id == Id);
            if (selectCategory == null)
            {
                data = new
                {
                    status = 404,
                    message = "Id doesn't exist",
                    response = ""

                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            var selecSubCategory = db.SubCategories.Where(sub => sub.CategoryId == Id)
                .Select(sub => new
                {
                    sub.Id,
                    sub.Name,
                })
                .ToList();
            data = new
            {
                status = 200,
                message = "Success.",
                response = selecSubCategory

            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/AddProducts/Create
        public ActionResult Create()
        {
            List<Category> CategoryId = db.Categories.ToList();
            ViewBag.CategoryId = new SelectList(CategoryId, "Id", "Name");
            return View();
        }
        public JsonResult GetSubCategoryList(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SubCategory> SubCatList = db.SubCategories.Where(s => s.CategoryId == Id).ToList();
            return Json(SubCatList, JsonRequestBehavior.AllowGet);
        }

        private int CheckMarkaName(string markaname)
        {
            Marka selectedmarka = db.Markas.FirstOrDefault(mr => mr.MarkaName.ToUpper() == markaname.ToUpper());
            if (selectedmarka==null)
            {
              Marka newMarka= db.Markas.Add(new Marka
                {
                    MarkaName = markaname
                });

                db.SaveChanges();
                return newMarka.Id;
            }
            return selectedmarka.Id;
        }
        // POST: Admin/AddProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Count,SubCategoryId,Discount,AddDate,Image")] Product product,string marka,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                
                if (Photo!=null)
                {
                    if (
                        Photo.ContentType.ToLower()=="image/jpg" ||
                        Photo.ContentType.ToLower()=="image/png" ||
                        Photo.ContentType.ToLower() == "image/gif" ||
                        Photo.ContentType.ToLower() == "image/jpeg")
                    {
                        WebImage image = new WebImage(Photo.InputStream);
                        FileInfo photoInfo = new FileInfo(Photo.FileName);
                        string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                        image.Save("~/Uploads/ProductImage/" + newPhoto);
                        product.Image = "/Uploads/ProductImage/" + newPhoto;
                    }
                }
                product.MarkaId= CheckMarkaName(marka);
                product.AddDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = db.Categories.ToList();
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", product.SubCategoryId);
            return View(product);
        }

        // GET: Admin/AddProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", product.SubCategoryId);
            ViewBag.CategoryId = db.Categories.ToList();

            return View(product);
        }

        // POST: Admin/AddProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Count,SubCategoryId,Discount,AddDate,Image")] Product product, int? id,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                var productContents = db.Products.SingleOrDefault(m => m.Id== id);
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(productContents.Image)))
                    {
                        System.IO.File.Delete(Server.MapPath(productContents.Image));
                    }
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Save("~/Uploads/ProductImage/" + newPhoto);
                    productContents.Image= "/Uploads/ProductImage/" + newPhoto;
                }

                productContents.Name = product.Name;
                productContents.Price= product.Price;
                productContents.AddDate = DateTime.Now;
                productContents.Count = product.Count;
                productContents.Discount = product.Discount;
                productContents.SubCategoryId= product.SubCategoryId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", product.SubCategoryId);
            ViewBag.CategoryId = db.Categories.ToList();

            return View(product);
        }

        // GET: Admin/AddProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/AddProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult AddImages(int? id)
        {
            if (id == null) return HttpNotFound();
            Product selectPro = db.Products.FirstOrDefault(pr => pr.Id == id);
            if (selectPro == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            return View(selectPro);
        }
        [HttpPost]
         public ActionResult UploadsProductImages(int? id, HttpPostedFileBase[] productpictures)
        {
            
            
            foreach (var pp in productpictures)
            {
              
                WebImage image = new WebImage(pp.InputStream);
                FileInfo photoInfo = new FileInfo(pp.FileName);
                string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                ProductImage proPicture = new ProductImage();

                image.Save("~/Uploads/ProductImage/" + newPhoto);
                proPicture.Image = "/Uploads/ProductImage/" + newPhoto;

                proPicture.ProductId = (int)id;
                db.ProductImages.Add(proPicture);
                db.SaveChanges();
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public ActionResult GetProductImages(int? id)
        {
            if (id == null) return HttpNotFound();
            
            List<ProductImage> proImage = db.ProductImages.Where(pr => pr.ProductId== id).ToList();
            return PartialView("_ProductPict",proImage);


        }
        public ActionResult RemoveProductPicture(int? id)
        {
            if (id == null) return HttpNotFound();
            db.ProductImages.Remove(db.ProductImages.Find(id));
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
