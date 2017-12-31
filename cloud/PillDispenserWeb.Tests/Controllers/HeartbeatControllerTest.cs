using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PillDispenserWeb.Configuration;
using PillDispenserWeb.Controllers;
using PillDispenserWeb.Logic.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PillDispenserWeb.Tests.Controllers
{
    [TestFixture]
    class HeartbeatControllerTest
    {
        #region Controller and Mocks
        private HeartbeatImpl heartbeat;
        private HeartbeatController hbController;
        #endregion

        #region Setup and Teardown
        [SetUp]
        public void Setup()
        {
            var mockHeartbeatJobController = new Mock<IRecurringJobManager>();
            var mockConfig = new HeartbeatConfig { IntervalMinutes = Int32.MaxValue };
            var mockOptions = Options.Create<HeartbeatConfig>(mockConfig);
            heartbeat = new HeartbeatImpl(mockOptions, mockHeartbeatJobController.Object);
            hbController = new HeartbeatController(heartbeat);
        }
        #endregion

        #region Tests
        [Test]
        public void Heartbeat_AddsToCurrentSet()
        {
            hbController.HeartbeatFromDevice("1");
            hbController.HeartbeatFromDevice("2");
            hbController.HeartbeatFromDevice("3");
            Assert.That(heartbeat.CurrentIterationDeviceIds.Contains("1"));
            Assert.That(heartbeat.CurrentIterationDeviceIds.Contains("2"));
            Assert.That(heartbeat.CurrentIterationDeviceIds.Contains("3"));
            Assert.AreEqual(heartbeat.CurrentIterationDeviceIds.Count, 3);

            heartbeat.HeartbeatTask();
            Assert.That(heartbeat.LastIterationDeviceIds.Contains("1"));
            Assert.That(heartbeat.LastIterationDeviceIds.Contains("2"));
            Assert.That(heartbeat.LastIterationDeviceIds.Contains("3"));
            Assert.AreEqual(heartbeat.LastIterationDeviceIds.Count, 3);
            Assert.IsEmpty(heartbeat.CurrentIterationDeviceIds);

            hbController.HeartbeatFromDevice("1");
            hbController.HeartbeatFromDevice("2");
            Assert.That(heartbeat.CurrentIterationDeviceIds.Contains("1"));
            Assert.That(heartbeat.CurrentIterationDeviceIds.Contains("2"));
            Assert.AreEqual(heartbeat.CurrentIterationDeviceIds.Count, 2);
            Assert.AreEqual(heartbeat.LastIterationDeviceIds.Count, 3);

            heartbeat.HeartbeatTask();
            Assert.That(heartbeat.LastIterationDeviceIds.Contains("1"));
            Assert.That(heartbeat.LastIterationDeviceIds.Contains("2"));
            Assert.AreEqual(heartbeat.LastIterationDeviceIds.Count, 2);
            Assert.That(heartbeat.LastMissingDeviceIds.Contains("3"));
            Assert.AreEqual(heartbeat.LastMissingDeviceIds.Count, 1);

            heartbeat.HeartbeatTask();
            Assert.That(heartbeat.LastMissingDeviceIds.Contains("1"));
            Assert.That(heartbeat.LastMissingDeviceIds.Contains("2"));
            Assert.AreEqual(heartbeat.LastMissingDeviceIds.Count, 2);
            Assert.IsEmpty(heartbeat.CurrentIterationDeviceIds);
            Assert.IsEmpty(heartbeat.LastIterationDeviceIds);

        }

        #endregion
    }
}
