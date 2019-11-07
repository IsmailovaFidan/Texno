using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TexnoGallery.Models;

namespace TexnoGallery.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    public class OrdersController : Controller
    {
        TexnoGalleryEntities db = new TexnoGalleryEntities();
        // GET: Admin/Orders
        public ActionResult Index()
        {

            return View(db.ApplicationUsers.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Order> appUser = db.Orders.Where(ap => ap.UserId == id).ToList();
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }
    }
}