using System;
using System.Text.Json.Serialization;

namespace Planday.Schedule
{
    public class Shift
    {
        public Shift(long id, long? employeeId, DateTime start, DateTime end)
        {
            Id = id;
            EmployeeId = employeeId;
            Start = start;
            End = end;
        }
        
        public Shift()
        {
            
        }

        public long Id { get; }
        public long? EmployeeId { get; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string EmployeeEmail { get; set; }
    }    
}

