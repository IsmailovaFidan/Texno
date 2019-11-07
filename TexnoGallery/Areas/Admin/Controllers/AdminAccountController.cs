using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TexnoGallery.Models;

namespace TexnoGallery.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        TexnoGalleryEntities db = new TexnoGalleryEntities();
        
        public ActionResult Login()
        {
            return View();
        }
        // GET: Admin/AdminAccount
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if(email!=string.Empty && password != string.Empty)
            {
                AdminsTB selectAdmin = db.AdminsTBs.Find(1);
                if(selectAdmin.Email==email && selectAdmin.Password==password)
                {
                    Session["adminLogged"] = selectAdmin;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Email və ya Parol səhvdir!";
                }
                
            }
            else
            {
                ViewBag.Error = "Zəhmət olmasa bütün xanaları doldurun!";
            }

            return View();
        }
        [HttpGet, Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}