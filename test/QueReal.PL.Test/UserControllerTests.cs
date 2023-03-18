using Microsoft.AspNetCore.Mvc;
using QueReal.BLL.Interfaces;
using QueReal.PL.Controllers;
using QueReal.PL.Models.User;

namespace QueReal.PL.Test
{
	[TestFixture]
	public class UserControllerTests
	{
		private LoginFormModel loginFormModel;
		private RegisterFormModel registerFormModel;

		private Mock<IUserService> userServiceMock;

		private UserController userController;

		[SetUp]
		public void SetUp()
		{
			loginFormModel = CreateLoginFormModel();
			registerFormModel = CreateRegisterFormModel();

			userServiceMock = new();

			userController = new(userServiceMock.Object);
		}

		[Test]
		public void Login_Get_ReturnViewResult()
		{
			var result = userController.Login();

			AssertCorrectViewResult(result, null, null);
		}

		[Test]
		public async Task Login_Post_WhenModelStateNotIsValid_ReturnViewResult()
		{
			SetModelStateIsValid(false);
			SetSignInSuccessful(true);

			var result = await userController.Login(loginFormModel);

			AssertCorrectViewResult(result, null, loginFormModel);
		}

		[Test]
		public async Task Login_Post_WhenSignInFailed_ReturnViewResult()
		{
			SetModelStateIsValid(true);
			SetSignInSuccessful(false);

			var result = await userController.Login(loginFormModel);

			AssertCorrectViewResult(result, null, loginFormModel);
		}

		[Test]
		public async Task Login_Post_WhenSignInSuccessful_ReturnRedirectToActionResult()
		{
			SetModelStateIsValid(true);
			SetSignInSuccessful(true);

			var result = await userController.Login(loginFormModel);

			AssertCorrectRedirectToActionResult(result, "Home", "Index");
		}

		[Test]
		public void Register_Get_ReturnViewResult()
		{
			var result = userController.Register();

			AssertCorrectViewResult(result, null, null);
		}

		[Test]
		public async Task Register_Post_WhenModelStateNotIsValid_ReturnViewResult()
		{
			SetModelStateIsValid(false);
			SetRegisterSuccessful(true);

			var result = await userController.Register(registerFormModel);

			AssertCorrectViewResult(result, null, registerFormModel);
		}

		[Test]
		public async Task Register_Post_WhenSignInFailed_ReturnViewResult()
		{
			SetModelStateIsValid(true);
			SetRegisterSuccessful(false);

			var result = await userController.Register(registerFormModel);

			AssertCorrectViewResult(result, null, registerFormModel);
		}

		[Test]
		public async Task Register_Post_WhenSignInSuccessful_ReturnRedirectToActionResult()
		{
			SetModelStateIsValid(true);
			SetRegisterSuccessful(true);

			var result = await userController.Register(registerFormModel);

			AssertCorrectRedirectToActionResult(result, "User", "Login");
		}

		[Test]
		public async Task Logout_Get_CallSignOut()
		{
			var result = await userController.Logout();

			userServiceMock.Verify(x => x.SignOutAsync(), Times.Once);
		}
		
		[Test]
		public async Task Logout_Get_ReturnRedirectToActionResult()
		{
			var result = await userController.Logout();

			AssertCorrectRedirectToActionResult(result, "Home", "Index");
		}

		private void SetModelStateIsValid(bool isValid)
		{
			if (!isValid)
			{
				userController.ModelState.AddModelError(string.Empty, "Test error");
			}
		}

		private void SetSignInSuccessful(bool isSuccessful)
		{
			userServiceMock
				.Setup(
					x => x.SignInAsync(
						loginFormModel.Email,
						loginFormModel.Password,
						loginFormModel.Remember))
				.ReturnsAsync(isSuccessful);
		}
		private void SetRegisterSuccessful(bool isSuccessful)
		{
			userServiceMock
				.Setup(
					x => x.RegisterAsync(
						registerFormModel.Email,
						registerFormModel.Password))
				.ReturnsAsync(isSuccessful);
		}

		private static void AssertCorrectViewResult(
			ActionResult actionResult,
			string expectedViewName,
			object expectedModel)
		{
			actionResult.Should().BeOfType<ViewResult>();

			var viewResult = (ViewResult)actionResult;
			viewResult.ViewName.Should().Be(expectedViewName);
			viewResult.Model.Should().Be(expectedModel);
		}

		private static void AssertCorrectRedirectToActionResult(
			ActionResult result,
			string controllerName,
			string actionName)
		{
			result.Should().BeOfType<RedirectToActionResult>();

			var redirectResult = (RedirectToActionResult)result;
			redirectResult.ControllerName.Should().Be(controllerName);
			redirectResult.ActionName.Should().Be(actionName);
		}

		private static LoginFormModel CreateLoginFormModel()
		{
			return new()
			{
				Email = "test@example.com",
				Password = "password",
				Remember = true
			};
		}
		
		private static RegisterFormModel CreateRegisterFormModel()
		{
			return new()
			{
				Email = "test@example.com",
				Password = "password",
				ConfirmPassword = "password"
			};
		}
	}
}
