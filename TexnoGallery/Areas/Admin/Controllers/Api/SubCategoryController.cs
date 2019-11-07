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

    public class SubCategoryController : ApiController
    {
        private TexnoGalleryEntities _context;

        public SubCategoryController()
        {
            _context = new TexnoGalleryEntities();

        }
        public IHttpActionResult GetSubCategories(string query = null)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var subcategoryquery = _context.SubCategories.Where(c => c.Name.Contains(query));
                return Ok(subcategoryquery);

            }
            return NotFound();
        }
            public SubCategory GetSubCategory(int? id)
        {
            var subcategory = _context.SubCategories.SingleOrDefault(sb => sb.Id == id);
            if (subcategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return subcategory;
        }
        [HttpPost]
        public SubCategory CreateSubCategory(SubCategory subCategory)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.SubCategories.Add(subCategory);
            _context.SaveChanges();
            return subCategory;
        }
        [HttpPut]
        public void UpdateSubCategory(int id, SubCategory subCategory)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var subcategoryDB = _context.SubCategories.SingleOrDefault(c => c.Id == id);
            if (subcategoryDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            subcategoryDB.Name = subCategory.Name;

            _context.SaveChanges();
        }
        //Delete/api/products/1
        [HttpDelete]
        public void DeleteSubCategory(int id)
        {
            var SubCategoryDB = _context.SubCategories.SingleOrDefault(c => c.Id == id);
            if (SubCategoryDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.SubCategories.Remove(SubCategoryDB);
            _context.SaveChanges();

        }
    }
}
