using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TexnoGallery.Models;

namespace TexnoGallery.Controllers
{
    public class OrdersProductController : Controller
    {
        TexnoGalleryEntities db = new TexnoGalleryEntities();
        // GET: OrdersProduct
        [HttpPost]
        public ActionResult Index(int? Id ,string quantity)
        {
            object data=null;
            if (Id == null) {
                data = new
                {
                    status = 404,
                    message = "Id not Found",
                    response = "",
                    Url = "Home/Index"
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            };

            var prc = db.Products.Select(pr=>new {
                 pr.Id,
                 pr.Name,
                 pr.Price,
                
            }).FirstOrDefault(pc=>pc.Id==Id);
            if (prc == null)
            {
                data = new
                {
                    status = 404,
                    message = "Id doesn't exist",
                    response = "",
                    Url= "Home/Index"
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            };
            if (!(Session["user"] is ApplicationUser appUser))
            {
                return RedirectToAction("Index", "Home");
            }
            if (appUser!=null)
            {
                int quant = Convert.ToInt32(quantity);
                db.Orders.Add(new Order
                {
                    ProductId = prc.Id,
                    OrderDate = DateTime.Now,
                    Quantity = quant,
                    UserId = appUser.Id,
                    Price=prc.Price,
                    TotalPrice=quant*prc.Price
                    
                });
                db.SaveChanges();
                data = new
                {
                    status = 200,
                    message = "Succes",
                    response = prc
                };
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }   
}