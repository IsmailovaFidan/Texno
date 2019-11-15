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
    public class CategoryImageController : Controller
    {
        private TexnoGalleryEntities db = new TexnoGalleryEntities();

        // GET: Admin/CategoryImage
        public ActionResult Index()
        {
           
            return View(db.ImageCategories.ToList());
        }

   
       

   

        // GET: Admin/CategoryImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageCategory imageCategory = db.ImageCategories.Find(id);
            if (imageCategory == null)
            {
                return HttpNotFound();
            }
            return View(imageCategory);
        }

        // POST: Admin/CategoryImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,[Bind(Include = "Id,Name,Description,Image")] ImageCategory imageCategory,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                ImageCategory selected = db.ImageCategories.SingleOrDefault(nt => nt.Id == id);
                if (Photo != null)
                {
                    WebImage image = new WebImage(Photo.InputStream);
                    FileInfo photoInfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo;
                    image.Save("~/Uploads/ProjectImage/" + newPhoto);
                    imageCategory.Image = "/Uploads/ProjectImage/" + newPhoto;
                }
                selected.Image = imageCategory.Image;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imageCategory);
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
