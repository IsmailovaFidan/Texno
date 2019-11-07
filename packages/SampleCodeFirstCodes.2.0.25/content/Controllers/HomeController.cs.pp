using $rootnamespace$.Context;
using System.Linq;
using System.Web.Mvc;

namespace $rootnamespace$.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DatabaseContext db = new DatabaseContext();
            db.Customers.ToList();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
