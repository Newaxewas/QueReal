using QueReal.BLL.Interfaces;
using QueReal.PL.Controllers;
using QueReal.PL.Models.User;

namespace QueReal.PL.Test
{
    [TestFixture]
    public class UserControllerTests
    {
        private LoginRequest loginRequest;
        private RegisterRequest registerRequest;

        private Mock<IUserService> userServiceMock;

        private UserController userController;

        [SetUp]
        public void SetUp()
        {
            loginRequest = CreateLoginRequest();
            registerRequest = CreateRegisterRequest();

            userServiceMock = new();

            userController = new(userServiceMock.Object);
        }

        [Test]
        public async Task Login_Post_WhenSignInFailed_ReturnUnauthorizedResult()
        {
            SetSignInSuccessful(false);

            var result = await userController.Login(loginRequest);

            PlTestHelper.AssertCorrectUnauthorizedResult(result);
        }

        [Test]
        public async Task Login_Post_WhenSignInSuccessful_ReturnOkResult()
        {
            SetSignInSuccessful(true);

            var result = await userController.Login(loginRequest);

            PlTestHelper.AssertCorrectOkResult(result);
        }

        [Test]
        public async Task Register_Post_WhenSignInFailed_ReturnBadRequestResult()
        {
            SetRegisterSuccessful(false);

            var result = await userController.Register(registerRequest);

            PlTestHelper.AssertCorrectBadRequestResult(result);
        }

        [Test]
        public async Task Register_Post_WhenSignInSuccessful_ReturnOkResult()
        {
            SetRegisterSuccessful(true);

            var result = await userController.Register(registerRequest);

            PlTestHelper.AssertCorrectOkResult(result);
        }

        [Test]
        public async Task Logout_Get_CallSignOut()
        {
            var result = await userController.Logout();

            userServiceMock.Verify(x => x.SignOutAsync(), Times.Once);
        }

        [Test]
        public async Task Logout_Get_ReturnOkResult()
        {
            var result = await userController.Logout();

            PlTestHelper.AssertCorrectOkResult(result);
        }

        private void SetSignInSuccessful(bool isSuccessful)
        {
            userServiceMock
                .Setup(
                    x => x.SignInAsync(
                        loginRequest.Email,
                        loginRequest.Password,
                        loginRequest.Remember))
                .ReturnsAsync(isSuccessful);
        }
        private void SetRegisterSuccessful(bool isSuccessful)
        {
            userServiceMock
                .Setup(
                    x => x.RegisterAsync(
                        registerRequest.Email,
                        registerRequest.Password))
                .ReturnsAsync(isSuccessful);
        }

        private static LoginRequest CreateLoginRequest()
        {
            return new()
            {
                Email = "test@example.com",
                Password = "password",
                Remember = true
            };
        }

        private static RegisterRequest CreateRegisterRequest()
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
