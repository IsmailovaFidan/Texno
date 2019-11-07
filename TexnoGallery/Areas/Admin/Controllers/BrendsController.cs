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
    public class BrendsController : Controller
    {
        private TexnoGalleryEntities db = new TexnoGalleryEntities();

        // GET: Admin/Brends
        public ActionResult Index()
        {
            return View(db.Brends.ToList());
        }

       

        // GET: Admin/Brends/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,BrendImg")] Brend brend,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {   
                    if (Photo != null)
                    {
                        WebImage image = new WebImage(Photo.InputStream);
                        FileInfo photoInfo = new FileInfo(Photo.FileName);
                        string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                        image.Save("~/Uploads/Brands/" + newPhoto);
                        brend.BrendImg = "/Uploads/Brands/" + newPhoto;
                    }
                db.Brends.Add(brend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brend);
        }

        // GET: Admin/Brends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brend brend = db.Brends.Find(id);
            if (brend == null)
            {
                return HttpNotFound();
            }
            return View(brend);
        }

        // POST: Admin/Brends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,[Bind(Include = "id,BrendImg")] Brend brend,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                Brend selected = db.Brends.SingleOrDefault(br => br.id == id);
                if (Photo != null)
                {
                    WebImage image = new WebImage(Photo.InputStream);
                    FileInfo photoinfo = new FileInfo(Photo.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoinfo;
                    image.Save("~/Uploads/Brands/" + newPhoto);
                    brend.BrendImg = "/Uploads/Brands/" + newPhoto;
                }
                selected.BrendImg = brend.BrendImg;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brend);
        }

        // GET: Admin/Brends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brend brend = db.Brends.Find(id);
            if (brend == null)
            {
                return HttpNotFound();
            }
            return View(brend);
        }

        // POST: Admin/Brends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brend brend = db.Brends.Find(id);
            db.Brends.Remove(brend);
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
