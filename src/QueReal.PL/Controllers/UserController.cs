using Microsoft.AspNetCore.Mvc;
using QueReal.PL.Models.User;

namespace QueReal.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(null);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var signedIn = await userService.SignInAsync(formModel.Email, formModel.Password, formModel.Remember);

                if (signedIn)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect email or password!");
                }
            }

            return View(formModel);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(null);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var isRegistred = await userService.RegisterAsync(formModel.Email, formModel.Password);

                if (isRegistred)
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong, please recheck your data");
                }
            }

            return View(formModel);
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await userService.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
