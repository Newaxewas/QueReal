using Microsoft.AspNetCore.Mvc;

namespace QueReal.PL.Controllers
{
	public class ExceptionController : Controller
	{
		[HttpGet]
		public ActionResult AccessDenied() 
		{
			return View();
		}
		
		[HttpGet]
		public ActionResult NotFound() 
		{
			return View();
		}
	}
}
