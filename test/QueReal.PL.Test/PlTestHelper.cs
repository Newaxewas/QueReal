using Microsoft.AspNetCore.Mvc;

namespace QueReal.PL.Test
{
    internal static class PlTestHelper
    {
        public static void AssertCorrectOkResult(ActionResult result)
        {
            result.Should().BeOfType<OkResult>();
        }

        public static void AssertCorrectUnauthorizedResult(ActionResult result)
        {
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        public static void AssertCorrectBadRequestResult(ActionResult result)
        {
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}