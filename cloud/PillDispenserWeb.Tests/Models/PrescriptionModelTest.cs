using NUnit.Framework;
using PillDispenserWeb.Models.DataTypes;
using PillDispenserWeb.Models.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PillDispenserWeb.Tests.Models
{
    [TestFixture]
    class PrescriptionModelTest
    {
        [Test]
        public void Prescription_Recurrence_ExpectedNumDoses()
        {
            // 1 year long recurrence, happening every day
            var recurrenceStart = new DateTimeOffset(2017, 01, 01, 0, 0, 0, TimeSpan.Zero);
            var recurrenceEnd = new DateTimeOffset(2018, 01, 01, 0, 0, 0, TimeSpan.Zero);
            var recurrenceInterval = new TimeSpan(1, 0, 0, 0); // one day
            var recurrence = new Prescription.Recurrence
            {
                Start = recurrenceStart,
                End = recurrenceEnd,
                Interval = recurrenceInterval,
                Doses = new List<Dose>(), // empty list
            };
            Assert.AreEqual(365, recurrence.GetExpectedNumberOfDoses(DateTimeOffset.MinValue, DateTimeOffset.MaxValue));
            Assert.AreEqual(360, recurrence.GetExpectedNumberOfDoses(recurrenceStart.AddDays(5), DateTimeOffset.MaxValue));
            // adding less than Interval time shouldn't change result
            Assert.AreEqual(5, recurrence.GetExpectedNumberOfDoses(DateTimeOffset.MinValue, recurrenceStart.AddDays(5).AddHours(1)));
            Assert.AreEqual(5, recurrence.GetExpectedNumberOfDoses(DateTimeOffset.MinValue, recurrenceStart.AddDays(5).AddHours(6)));
            Assert.AreEqual(5, recurrence.GetExpectedNumberOfDoses(DateTimeOffset.MinValue, recurrenceStart.AddDays(5).AddHours(15)));
            // end date is handled correctly
            Assert.AreEqual(6, recurrence.GetExpectedNumberOfDoses(DateTimeOffset.MinValue, recurrenceStart.AddDays(6)));
            // both end and start dates are handled correctly
            Assert.AreEqual(1, recurrence.GetExpectedNumberOfDoses(recurrenceStart.AddDays(5), recurrenceStart.AddDays(6)));
            // negative date ranges return zero
            Assert.AreEqual(0, recurrence.GetExpectedNumberOfDoses(recurrenceStart.AddDays(10), recurrenceStart.AddDays(1)));
        }
        [Test]
        public void Prescription_NextRecurrence()
        {
            // 1 year long recurrence, happening every day
            var recurrenceStart = new DateTimeOffset(2017, 01, 01, 0, 0, 0, TimeSpan.Zero);
            var recurrenceEnd = new DateTimeOffset(2018, 01, 01, 0, 0, 0, TimeSpan.Zero);
            var recurrenceInterval = new TimeSpan(1, 0, 0, 0); // one day
            var recurrence_daily = new Prescription.Recurrence
            {
                Start = recurrenceStart,
                End = recurrenceEnd,
                Interval = recurrenceInterval,
                Doses = new List<Dose>(), // empty list
            };
            var recurrence_7hours = new Prescription.Recurrence
            {
                Start = recurrenceStart,
                End = recurrenceEnd,
                Interval = new TimeSpan(7, 0, 0), // every 7 hours
                Doses = new List<Dose>(),
            };
            var prescription = new Prescription { Recurrences = new List<Prescription.Recurrence> { recurrence_daily } };
            var currentTime = recurrenceStart.AddSeconds(1);
            var expectedNextRecurrence = new DateTimeOffset(2017, 01, 02, 0, 0, 0, TimeSpan.Zero);
            Assert.AreEqual(expectedNextRecurrence, prescription.NextRecurrence(currentTime));
            prescription.Recurrences = new List<Prescription.Recurrence> { recurrence_daily, recurrence_7hours };
            
            Assert.AreEqual(recurrenceStart.AddHours(7), prescription.NextRecurrence(currentTime));
        }

    }
}