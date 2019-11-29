using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TexnoGallery.Models;

namespace TexnoGallery.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    public class CategoryTextController : Controller
    {
        private TexnoGalleryEntities db = new TexnoGalleryEntities();

        // GET: Admin/CategoryText
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Admin/CategoryText/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        //private int CheckCategoryName(string catname)
        //{
        //    Category selectedCategory = db.Categories.FirstOrDefault(mr => mr.Name.ToUpper() == catname.ToUpper());
        //    if (selectedCategory == null)
        //    {
        //        Category newCategory= db.Categories.Add(new Category
        //        {
        //            Name = catname
        //        });
        //        db.SaveChanges();
        //        return newCategory.Id;
        //    }
        //    return selectedCategory.Id;
        //}

        // GET: Admin/CategoryText/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryText/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryImg")] Category category, HttpPostedFileBase Photo)
        {
            Category selectedCategory = db.Categories.FirstOrDefault(ct => ct.Name.ToLower() == category.Name.ToLower());
            if (ModelState.IsValid)
            {
                if (selectedCategory==null && Photo==null)
                {
                    WebImage image = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo;
                    image.Save("~/Uploads/" + newPhoto);
                    category.CategoryImg = "/Uploads/" + newPhoto;

                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
                
            }

            return View(category);
        }

        // GET: Admin/CategoryText/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoryText/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,[Bind(Include = "Id,CategoryImg,Name")] Category category, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                Category selected = db.Categories.SingleOrDefault(nt => nt.Id == id);

                if (Photo != null)
                {

                    WebImage image = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo;
                    image.Save("~/Uploads/" + newPhoto);
                    category.CategoryImg = "/Uploads/" + newPhoto;
                }
                selected.CategoryImg = category.CategoryImg;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/CategoryText/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoryText/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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
