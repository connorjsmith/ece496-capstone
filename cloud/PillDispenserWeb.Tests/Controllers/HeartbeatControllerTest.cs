using Moq;
using NUnit.Framework;
using PillDispenserWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PillDispenserWeb.Tests.Controllers
{
    [TestFixture]
    class HeartbeatControllerTest
    {
        #region Controller and Mocks
        private HeartbeatController hbController;
        private SortedSet<string> controllerLastIterationDeviceIds;
        private SortedSet<string> controllerCurrentIterationDeviceIds;
        private SortedSet<string> controllerLastMissingDeviceIds;
        #endregion

        #region Setup and Teardown
        [SetUp]
        public void Setup()
        {
            hbController = new HeartbeatController();
        }
        #endregion

        #region Tests
        [Test]
        public void Heartbeat_AddsToCurrentSet()
        {
            hbController.HeartbeatFromDevice("1");
            hbController.HeartbeatFromDevice("2");
            hbController.HeartbeatFromDevice("3");
            Assert.That(hbController.CurrentIterationDeviceIds.Contains("1"));
            Assert.That(hbController.CurrentIterationDeviceIds.Contains("2"));
            Assert.That(hbController.CurrentIterationDeviceIds.Contains("3"));
            Assert.AreEqual(hbController.CurrentIterationDeviceIds.Count, 3);

            hbController.HeartbeatTask();
            Assert.That(hbController.LastIterationDeviceIds.Contains("1"));
            Assert.That(hbController.LastIterationDeviceIds.Contains("2"));
            Assert.That(hbController.LastIterationDeviceIds.Contains("3"));
            Assert.AreEqual(hbController.LastIterationDeviceIds.Count, 3);
            Assert.IsEmpty(hbController.CurrentIterationDeviceIds);

            hbController.HeartbeatFromDevice("1");
            hbController.HeartbeatFromDevice("2");
            Assert.That(hbController.CurrentIterationDeviceIds.Contains("1"));
            Assert.That(hbController.CurrentIterationDeviceIds.Contains("2"));
            Assert.AreEqual(hbController.CurrentIterationDeviceIds.Count, 2);
            Assert.AreEqual(hbController.LastIterationDeviceIds.Count, 3);

            hbController.HeartbeatTask();
            Assert.That(hbController.LastIterationDeviceIds.Contains("1"));
            Assert.That(hbController.LastIterationDeviceIds.Contains("2"));
            Assert.AreEqual(hbController.LastIterationDeviceIds.Count, 2);
            Assert.That(hbController.LastMissingDeviceIds.Contains("3"));
            Assert.AreEqual(hbController.LastMissingDeviceIds.Count, 1);

            hbController.HeartbeatTask();
            Assert.That(hbController.LastMissingDeviceIds.Contains("1"));
            Assert.That(hbController.LastMissingDeviceIds.Contains("2"));
            Assert.AreEqual(hbController.LastMissingDeviceIds.Count, 2);
            Assert.IsEmpty(hbController.CurrentIterationDeviceIds);
            Assert.IsEmpty(hbController.LastIterationDeviceIds);

        }

        #endregion
    }
}
