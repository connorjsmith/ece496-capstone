using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Moq;
using NUnit.Framework;
using PillDispenserWeb.Models.DataTypes;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PillDispenserWeb.Models;
using PillDispenserWeb.Models.Relations;

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

        private HomeController controller;

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
            // TODO we can use GenFu here to create good test sample data
            var fakePatientList = new List<Patient>()
            {
                new Patient{FirstName = "Amy", LastName = "Adams", PatientId = 1, EmailAddress="Amy.Adams@email.com"},
                new Patient{FirstName = "Bert", LastName = "Benson", PatientId = 2, EmailAddress="Bert.Benson@email.com"},
                new Patient{FirstName = "Cooper", LastName = "Calvert", PatientId = 3, EmailAddress="Cooper.Calvert@email.com"}
            }.AsQueryable();

            var fakeDoctorList = new List<Doctor>()
            {
                new Doctor{FirstName = "Albert", LastName = "Alderson", DoctorId = 1, EmailAddress = "Albert.Alderson@email.com" },
                new Doctor{
                    FirstName = "Betty", LastName = "Brown", DoctorId = 2, EmailAddress = "Betty.Brown@email.com",
                },
                new Doctor{FirstName = "Calvin", LastName = "Crawford", DoctorId = 3, EmailAddress = "Calvin.Crawford@email.com"}
            }.AsQueryable();

            var mockDoctorSet = new Mock<DbSet<Doctor>>();
            var mockPatientSet = new Mock<DbSet<Patient>>();
            var contextBuilder = new DbContextOptionsBuilder();
            var appDataContext = new AppDataContext();

            mockDoctorSet.As<IQueryable<Doctor>>().Setup(d => d.Provider).Returns(fakeDoctorList.Provider);
            mockDoctorSet.As<IQueryable<Doctor>>().Setup(d => d.Expression).Returns(fakeDoctorList.Expression);
            mockDoctorSet.As<IQueryable<Doctor>>().Setup(d => d.ElementType).Returns(fakeDoctorList.ElementType);
            mockDoctorSet.As<IQueryable<Doctor>>().Setup(d => d.GetEnumerator()).Returns(() => fakeDoctorList.GetEnumerator());

            mockPatientSet.As<IQueryable<Patient>>().Setup(d => d.Provider).Returns(fakePatientList.Provider);
            mockPatientSet.As<IQueryable<Patient>>().Setup(d => d.Expression).Returns(fakePatientList.Expression);
            mockPatientSet.As<IQueryable<Patient>>().Setup(d => d.ElementType).Returns(fakePatientList.ElementType);
            mockPatientSet.As<IQueryable<Patient>>().Setup(d => d.GetEnumerator()).Returns(() => fakePatientList.GetEnumerator());

            appDataContext.Doctors = mockDoctorSet.Object;
            appDataContext.Patients = mockPatientSet.Object;

            controller = new HomeController(appDataContext);
        }
        #endregion
    }
}
