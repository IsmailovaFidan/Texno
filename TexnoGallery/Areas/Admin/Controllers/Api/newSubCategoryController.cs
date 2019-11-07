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

    public class newSubCategoryController : ApiController
    {
        TexnoGalleryEntities _context;
        public newSubCategoryController()
        {
            _context = new TexnoGalleryEntities();
        }
      
        [HttpPost]
        public IHttpActionResult CreateNewSubCategory(SubCategory subCategory,Category cat)
        {
            var category = _context.SubCategories.Single(c => c.CategoryId == subCategory.Id);
            var sbcat = _context.SubCategories.Where(s=>subCategory.Id==subCategory.Id);
           
            return Ok();
        }
    }
}
