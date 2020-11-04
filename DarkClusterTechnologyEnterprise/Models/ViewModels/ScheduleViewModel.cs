using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class ScheduleViewModel
    {
        public SelectList Hours { get; set; }
        public SelectList Minutes { get; set; }
        //public SelectList Days {get;set;}
        //public SelectList Months {get;set;}
        //public SelectList Years {get;set;}

    }
    public class NewTask
    {
        public string? Task { get; set; }
        public string? TaskDesc { get; set; }
        public TimeSpan TaskBeginTime { get; set; }
        public TimeSpan TaskEndTime { get; set; }
        public DateTime TaskBeginDate { get; set; }
        public DateTime TaskEndDate { get; set; }
    }
}
