using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueReal.PL.Models.User;

namespace QueReal.PL.Controllers
{
    [ApiController, Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult> Login(LoginFormModel formModel)
        {
            var signedIn = await userService.SignInAsync(formModel.Email, formModel.Password, formModel.Remember);

            return signedIn ? Ok() : Unauthorized("Incorrect email or password!");
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<ActionResult> Register(RegisterFormModel formModel)
        {
            var isRegistred = await userService.RegisterAsync(formModel.Email, formModel.Password);

            return isRegistred ? Ok() : BadRequest("Something went wrong, please recheck your data");
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await userService.SignOutAsync();

            return Ok();
        }
    }
}
