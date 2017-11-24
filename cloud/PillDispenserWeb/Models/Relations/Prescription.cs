using PillDispenserWeb.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.Relations
{
    public class Prescription
    {

        #region Data Model
        public IEnumerable<Recurrence> Recurrences { get; set; }
        public Doctor PrescribingDoctor { get; set; }
        public Medication Medication { get; set; }
        public string PrescriptionId { get; set; }
        #endregion 

        #region Helper Classes
        public class Recurrence
        {
            public string RecurrenceId { get; set; } // unused, but necessary for SQL
            public DateTimeOffset Start { get; set; }
            public DateTimeOffset End { get; set; }
            public TimeSpan Interval { get; set; }
            public IEnumerable<Dose> Doses { get; set; }
            public int GetExpectedNumberOfDoses(DateTimeOffset StartDate, DateTimeOffset EndDate)
            {
                var rangeEnd = EndDate < End ? EndDate : End;
                var rangeStart = Start < StartDate ? StartDate : Start;

                var numOccurencesBeforeRangeStart = Math.Floor((rangeStart - Start).TotalSeconds / Interval.TotalSeconds);
                var numOccurencesBeforeRangeEnd = Math.Floor((rangeEnd - Start).TotalSeconds / Interval.TotalSeconds);
                // Return at least 0 doses, covers negative cases above
                return Math.Max((int)(numOccurencesBeforeRangeEnd - numOccurencesBeforeRangeStart), 0);
            }
        }
        #endregion Helper Classes

        #region Helper Functions
        public DateTimeOffset NextRecurrence(DateTimeOffset now)
        {
            var activeRecurrences = Recurrences.Where(r => r.Start <= now && r.End >= now);
            var nextFutureRecurrences = activeRecurrences.Select(r =>
            {
                // Take the number of seconds since the first recurrence, divide that into a number of partial recurrences, round up and return to seconds
                return r.Start.AddSeconds(r.Interval.TotalSeconds * Math.Ceiling((now - r.Start).TotalSeconds / r.Interval.TotalSeconds));
            });

            // Sort the next recurrences and return the smallest one
            var nextRecurrence = nextFutureRecurrences.OrderBy(r => r).First();

            return nextRecurrence;
        }
        #endregion Helper Functions
    }
}
