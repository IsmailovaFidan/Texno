using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TexnoGallery.ViewModel.Default;
using TexnoGallery.Models;
namespace TexnoGallery.Controllers
{
    public class HomeController : Controller
    {
        TexnoGalleryEntities db = new TexnoGalleryEntities();

        public ActionResult Index()
        {

            var defaultModel = new DefaultViewModel
            {

                SlideImage = db.Slides.ToList(),
           
                BrendPhoto = db.Brends.ToList(),
                CategoryName = db.Categories.ToList(),
                productList = db.Products.OrderByDescending(pr=>pr.Id).Take(10).ToList(),
                aboutTech=db.AboutUs.FirstOrDefault()

            };
            return View(defaultModel);
        }
        public ActionResult SearchPro(string searchText)
        {
            var defaultModel = new DefaultViewModel
            {
                CategoryName = db.Categories.ToList(),
                aboutTech = db.AboutUs.FirstOrDefault(),
                productList = db.Products.Where(pr=>pr.SubCategory.Name.Contains(searchText) || pr.SubCategory.Category.Name.Contains(searchText)).Take(10).ToList(),
            };
            return View(defaultModel);
        }
        public ActionResult About()
        {
            var defaultModel = new DefaultViewModel
            {
                CategoryName = db.Categories.ToList(),

                aboutTech = db.AboutUs.FirstOrDefault(),
               

            };
            return View(defaultModel);
        }
        public ActionResult Contact()
        {
            var defaultModel = new DefaultViewModel
            {
                aboutTech = db.AboutUs.FirstOrDefault(),
                CategoryName = db.Categories.ToList(),
                contactTech = db.Contacts.Find(1)
            };
            return View(defaultModel);
        }
    }
}