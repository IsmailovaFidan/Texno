

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TexnoGallery.Models;
using TexnoGallery.ViewModel.Default;

namespace TexnoGallery.Controllers
{
   
    public class MainAccountController : Controller
    {

        TexnoGalleryEntities db = new TexnoGalleryEntities();
        public ActionResult Profiles()
        {
            ApplicationUser appUser = Session["user"] as ApplicationUser;
            if (appUser == null) return RedirectToAction("index","home");
            var defaultModel = new DefaultViewModel
            {
                CategoryName = db.Categories.ToList(),
                applicationUser=appUser 
            };

            return View(defaultModel);
        }
        [HttpPost]
        public async Task<JsonResult>  GetPassword(string password, string newpassword,string confirmpassword)
        {
            if (password!="" && newpassword!="" && confirmpassword!="")
            {
            object data = null;
            ApplicationUser userpass = Session["user"] as ApplicationUser;
            if (userpass != null)
            {
                if (Crypto.VerifyHashedPassword(userpass.Password,password ))
                {
                    userpass.Password =userpass.ConfirmPassword= Crypto.HashPassword(newpassword);
                    await db.SaveChangesAsync();
                    data = new {
                        status = 200,
                        message = "Success",
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);

                }

            }
            }

            return Json(false, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(ApplicationUser us, HttpPostedFileBase Photo)
        {
            if (!ModelState.IsValid) return View(us);

            if (Photo != null)  
            {

                WebImage image = new WebImage(Photo.InputStream);
                FileInfo photoInfo = new FileInfo(Photo.FileName);
                string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                image.Save("~/Uploads/UserImage/" + newPhoto);
                us.ProfileImage = "/Uploads/UserImage/" + newPhoto;
            }

            if (db.ApplicationUsers.Any(u => u.Email == us.Email))
            {
                ModelState.AddModelError("Email", "This email is duplicate");
                return View(us);
            }

            if (db.ApplicationUsers.Any(u => u.UserName == us.UserName))
            {
                ModelState.AddModelError("Email", "This username is duplicate");
                return View(us);
            }

            us.Password = us.ConfirmPassword = Crypto.HashPassword(us.Password);

            db.ApplicationUsers.Add(us);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Login));
        }
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        
        public ActionResult Login(UserLogin loginUser)
        {
            if (!ModelState.IsValid) return View(loginUser);

            ApplicationUser appUser = db.ApplicationUsers.FirstOrDefault(u => u.Email == loginUser.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("summary", "Email or password is invalid");
                return View(loginUser);
            }

            if (!Crypto.VerifyHashedPassword(appUser.Password, loginUser.Password))
            {
                ModelState.AddModelError("summary", "Email or password is invalid");
                return View(loginUser);
            }

            Session["user"] = appUser;

            return RedirectToAction("Index", "Home");
        }
           
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}