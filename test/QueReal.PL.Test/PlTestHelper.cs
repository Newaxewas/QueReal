using Microsoft.AspNetCore.Mvc;

namespace QueReal.PL.Test
{
    internal static class PlTestHelper
    {

        public static void AssertCorrectRedirectToActionResult(
            ActionResult result,
            string controllerName,
            string actionName)
        {
            result.Should().BeOfType<RedirectToActionResult>();

            var redirectResult = (RedirectToActionResult)result;
            redirectResult.ControllerName.Should().Be(controllerName);
            redirectResult.ActionName.Should().Be(actionName);
        }

        public static void AssertCorrectViewResult(
            ActionResult actionResult,
            string expectedViewName,
            object expectedModel)
        {
            actionResult.Should().BeOfType<ViewResult>();

            var viewResult = (ViewResult)actionResult;
            viewResult.ViewName.Should().Be(expectedViewName);
            viewResult.Model.Should().Be(expectedModel);
        }
    }
}