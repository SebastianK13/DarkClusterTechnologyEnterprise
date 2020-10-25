using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class PresenceViewModel
    {
        public PresenceViewModel()
        {
            Presences = new List<Presence>();
            Breaks = new List<Break>();
        }
        public List<Presence> Presences { get; set; }
        public List<AvgPresence> AvgPresences { get; set; }
        public List<Break> Breaks { get; set; }
        public List<AvgBreaks> AvgBreaks { get; set; }
        public double Seconds { get; set; }
        public double BreakLength { get; set; }
        public bool BreakActive { get; set; }
        //public bool WorkActive { get; set; }
        public List<GraphViewModel> DataModel { get; set; }
    }
    public class GraphViewModel 
    {
        public DateTime WorkBeginTime { get; set; }
        public double WorkTime { get; set; }
        public DateTime BreakStart { get; set; }
        public double BreakTime { get; set; }
    }
    public class AvgPresence
    {
        public DateTime DayOfWork { get; set; }
        public double AvgTime { get; set; }
        public int divider { get; set; }
    }
    public class AvgBreaks
    {
        public DateTime DayOfWork { get; set; }
        public double AvgBreakTime { get; set; }
        public int divider { get; set; }
    }
    public class EmployeeAmount
    {
        public DateTime Day { get; set; }
        public List<int> EmployeeList { get; set; }
    }
}
