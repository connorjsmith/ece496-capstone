using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PillDispenserWeb.Controllers;
using PillDispenserWeb.Logic;
using PillDispenserWeb.Models;
using PillDispenserWeb.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PillDispenserWeb.Models.Relations.Prescription;

namespace PillDispenserWeb.Tests.Controllers
{
    [TestFixture]
    class ReportingControllerTest
    {
        private ReportingController controller;
        private Mock<IHeartbeat> mockHeartbeatController;
        private AppDataContext mockDataContext;
        private IQueryable<Recurrence> mockRecurrenceRepo;
        private IQueryable<Dose> mockDoseRepo;
        private Recurrence mockRecurrence;
        private Mock<DbSet<Recurrence>> recurrenceDbSet;
        private Mock<DbSet<Dose>> doseDbSet;

        #region Setup
        [SetUp]
        public void Setup()
        {
            mockHeartbeatController = new Mock<IHeartbeat>();
            // mockHeartbeatController.Setup(c => c.AddHeartbeat(It.IsAny<string>()));
            mockHeartbeatController.Setup(c => c.AddHeartbeat("1"));

            mockRecurrence = new Recurrence { RecurrenceId = "1", Doses = new List<Dose>() };
            mockRecurrenceRepo = new List<Recurrence> { mockRecurrence }.AsQueryable();
            mockDoseRepo = new List<Dose>().AsQueryable();

            mockDataContext = new AppDataContext();

            // setup the mock database
            recurrenceDbSet = new Mock<DbSet<Recurrence>>();
            doseDbSet = new Mock<DbSet<Dose>>();
            // recurrenceDbSet
            // .Setup(d => d.FindAsync(It.IsAny<string>()))
            // .Returns(new Task<Recurrence>(() => null));
            recurrenceDbSet.As<IQueryable<Recurrence>>().Setup(m => m.Provider).Returns(mockRecurrenceRepo.Provider);
            recurrenceDbSet.As<IQueryable<Recurrence>>().Setup(m => m.Expression).Returns(mockRecurrenceRepo.Expression);
            recurrenceDbSet.As<IQueryable<Recurrence>>().Setup(m => m.ElementType).Returns(mockRecurrenceRepo.ElementType);
            recurrenceDbSet.As<IQueryable<Recurrence>>().Setup(m => m.GetEnumerator()).Returns(mockRecurrenceRepo.GetEnumerator());
            mockDataContext.Recurrence = recurrenceDbSet.Object;

            controller = new ReportingController(mockHeartbeatController.Object, mockDataContext);
        }
        #endregion Setup

        [Test(Description = "Missed endpoint adds a heartbeat")]
        public void MissedEndpointAddsHeartbeat()
        {
            var result = controller.RecordMedicationEvent("1", "1", "1", DateTimeOffset.Now, false);
            mockHeartbeatController.Verify(c => c.AddHeartbeat("1"));
            Assert.AreEqual("success", result.Value);
        }
        [Test(Description = "Taken endpoint adds a heartbeat")]
        public void TakenEndpointAddsHeartbeat()
        {
            var result = controller.RecordMedicationEvent("1", "1", "1", DateTimeOffset.Now, true);
            mockHeartbeatController.Verify(c => c.AddHeartbeat("1"));
            Assert.AreEqual("success", result.Value);

        }
        [Test(Description = "Missed and Taken endpoints are authenticated")]
        public void EndpointsAreAuthenticated()
        {

        }
        [Test(Description = "Missed and Taken endpoints add appropriate new Dose records")]
        public void EndpointsAddDoseRecords()
        {
            var result = controller.RecordMedicationEvent("1", "1", "1", DateTimeOffset.Now, false);
            Assert.AreEqual("success", result.Value);
            Assert.AreEqual(1, mockRecurrence.Doses.Count());
        }
    }
}
