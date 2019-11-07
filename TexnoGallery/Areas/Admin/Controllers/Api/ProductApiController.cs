using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TexnoGallery.Models;

namespace TexnoGallery.Areas.Admin.Controllers.Api
{
    public class ProductApiController : ApiController
    {
        TexnoGalleryEntities db = new TexnoGalleryEntities();
        //Get/api/products
        [DisableCors]
        public IEnumerable<Product> GetProducts()
        {
            return db.Products.ToList();
         }
        //Get/api/product/1
        public Product GetProduct(int id)
        {
            var pro = db.Products.SingleOrDefault(p => p.Id == id);
            if (pro == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return pro;
        }
        //Post/api/Products
        [HttpPost]
        public Product CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }
        [HttpPut]
        public void UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var ProductDb = db.Products.SingleOrDefault(c => c.Id == id);
            if (ProductDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ProductDb.Name = product.Name;
            ProductDb.Image = product.Image;
            ProductDb.AddDate = product.AddDate;
            ProductDb.Count = product.Count;
            ProductDb.Discount = product.Discount;
            ProductDb.Price = product.Price;
            ProductDb.SubCategory.Name = product.SubCategory.Name;


            db.SaveChanges();
        }
        //Delete/api/products/1
        [HttpDelete]
        public void DeletePro(int id)
        {
            var ProductDb = db.Products.SingleOrDefault(c => c.Id == id);
            if (ProductDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            db.Products.Remove(ProductDb);
            db.SaveChanges();

        }
    }
}
