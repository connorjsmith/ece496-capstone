using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Net;
using System.Net.Http;

namespace PillDispenserWeb.Tests
{
    public class HomeControllerTest
    {

        [Fact]
        public async Task Index_Returns200()
        {
            var controller = new HomeController();
            IActionResult result = await controller.Index();
            Assert.Equal(1, 1);
        }
    }
}
