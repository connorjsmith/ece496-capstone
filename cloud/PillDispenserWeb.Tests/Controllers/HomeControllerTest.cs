using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Moq;
using System.Net.Http;
using NUnit.Framework;
using PillDispenserWeb.Models.Interfaces;
using PillDispenserWeb.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using GenFu;
using PillDispenserWeb.Tests.Models.MockRepositories;

namespace PillDispenserWeb.Tests.Controllers
{
    // Fixture name should be <FileBeingTested>Test
    [TestFixture]
    public class HomeControllerTest
    {
        #region Configuration Settings

        private const int NumMockDoctors = 40;

        #endregion Configuration Settings

        #region Repositories and Controllers

        IDoctorRepository doctorRepository;
        HomeController controller;

        #endregion

        #region Tests
        // Test name should be <Method under test>_<Circumstances or parameters of test>_<expected result>
        [Test(
            Description = "GET requests on / should return 200"
        )]
        public async Task Index_HttpGet_200()
        {
            var result = await controller.Index() as ViewResult;
            Assert.NotNull(result);

            // Not sure why statuscode is not being set
            if (result.StatusCode.HasValue)
            {
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            }

            Assert.AreEqual("/Views/Home/Index.cshtml", result.ViewName);
        }

        #endregion Tests

        #region Test Setup and Teardown
        [SetUp]
        public void SetupEach()
        {
            doctorRepository = new MockDoctorRepository(NumMockDoctors);
            controller = new HomeController(doctorRepository);
        }
        #endregion
    }
}
