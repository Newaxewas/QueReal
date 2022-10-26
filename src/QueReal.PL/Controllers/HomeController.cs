using Microsoft.AspNetCore.Mvc;

namespace QueReal.PL.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index() 
        {
            return View();
        }
    }
}
