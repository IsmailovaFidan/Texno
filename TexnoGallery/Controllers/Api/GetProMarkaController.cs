using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TexnoGallery.Models;

namespace TexnoGallery.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class GetProMarkaController : ApiController
    {
        private TexnoGalleryEntities db = new TexnoGalleryEntities();

        // GET: api/GetProMarka
        public IQueryable<Product> GetProduct(int? catId, int? subCatId, int? markaId,int? minPrice,int? maxPrice)
        {

            if (subCatId == null)
                return db.Products.Where(pr => pr.SubCategory.CategoryId == catId && pr.Price >= minPrice && pr.Price <= maxPrice && pr.MarkaId == markaId);

            if (markaId == null)
            {
                 return db.Products.Where(pr => pr.SubCategory.CategoryId == catId && pr.SubCategoryId == subCatId && pr.Price >= minPrice && pr.Price <= maxPrice);
            }
            
            return db.Products.Where(pr=>pr.SubCategoryId==subCatId && pr.MarkaId==markaId && pr.Price>=minPrice && pr.Price<=maxPrice);
        }

        // GET: api/GetProMarka/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool ProductExists(int id)
        //{
        //    return db.Products.Count(e => e.Id == id) > 0;
        //}
    }
}