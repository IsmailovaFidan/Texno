using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TexnoGallery.Models;

namespace TexnoGallery.Areas.Admin.Controllers
{
    [AuthenticationFilter]

    public class OptionsController : Controller
    {
        private TexnoGalleryEntities db = new TexnoGalleryEntities();

        // GET: Admin/Options
        public ActionResult Index(int? id)
        {
            if (id == null) return HttpNotFound();
            var proOption = db.ProductOptions.Where(pr => pr.ProductId == id).ToList();
            if (proOption == null) return HttpNotFound();
            ViewBag.ProId = id;
            return View(proOption);
        }

        // GET: Admin/Options/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option options = db.Options.Find(id);
            if (options == null)
            {
                return HttpNotFound();
            }
            return View(options);
        }

        // GET: Admin/Options/Create
        public ActionResult Create(int? id)
        {
            if (id == null) return HttpNotFound();
            Product selectProduct = db.Products.FirstOrDefault(pr => pr.Id == id);
            if (selectProduct == null) return HttpNotFound();
            ViewBag.proList = selectProduct;
            ViewBag.optionList = db.ProductOptions.Where(opr => opr.ProductId == selectProduct.Id).ToList();
            return View();
        }

        // POST: Admin/Options/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Option options,int id)
        {
            ViewBag.proList = db.Products.ToList();
            if (ModelState.IsValid)
            {
               Option opt= db.Options.Add(options);
                db.SaveChanges();

                db.ProductOptions.Add(new ProductOption
                {
                    OptionsId = opt.Id,
                    ProductId=id
                });
                db.SaveChanges();
                return RedirectToAction("Index",new {id=id });
            }

            return View(options);
        }

        // GET: Admin/Options/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option options = db.Options.Find(id);
            if (options == null)
            {
                return HttpNotFound();
            }
            return View(options);
        }

        // POST: Admin/Options/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Option options)
        {
            if (ModelState.IsValid)
            {
                db.Entry(options).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(options);
        }

        // GET: Admin/Options/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option options = db.Options.Find(id);
            if (options == null)
            {
                return HttpNotFound();
            }
            return View(options);
        }

        // POST: Admin/Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Option options = db.Options.Find(id);
            db.Options.Remove(options);
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
