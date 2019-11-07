using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TexnoGallery.Models;
using TexnoGallery.ViewModel.Default;

namespace TexnoGallery.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        TexnoGalleryEntities db = new TexnoGalleryEntities();
        public ActionResult Index()
        {

            ViewBag.UsersCount = db.ApplicationUsers.Count();
            ViewBag.OrdersCount = db.Orders.Count();
            ViewBag.allUsers = db.ApplicationUsers.ToList();
            var defaultModel = new DefaultViewModel
            {
                OrderList = db.Orders.ToList(),
            };
            return View(defaultModel);
        }
    }
}