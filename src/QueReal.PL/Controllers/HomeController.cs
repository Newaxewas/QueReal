using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QueReal.PL.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet, AllowAnonymous]
        public ActionResult Index() 
        {
            return View();
        }
    }
}
