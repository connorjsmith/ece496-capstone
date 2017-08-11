using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Moq;
using System.Net.Http;
using NUnit.Framework;

namespace PillDispenserWeb.Tests.Controllers
{
    // Fixture name should be <FileBeingTested>Test
    [TestFixture]
    public class HomeControllerTest
    {

        // Test name should be <Method under test>_<Circumstances or parameters of test>_<expected result>
        [Test(
            Description = "GET requests on / should return 200"
        )]
        public async Task Index_HttpGet_200()
        {
            var controller = new HomeController(); // TODO: use Moq to mock out the controller, best practices?
            var result = await controller.Index() as ViewResult;
            Assert.NotNull(result);

            // Not sure why statuscode is not being set
            if (result.StatusCode.HasValue)
            {
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            }

            Assert.AreEqual("/Views/Home/Index.cshtml", result.ViewName);
        }
    }
}
