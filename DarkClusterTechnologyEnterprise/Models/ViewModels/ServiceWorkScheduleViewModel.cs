using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class NewServiceWork
    {
        [Required(ErrorMessage = "Task name is required")]
        public string? Task { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? TaskDesc { get; set; }
        [Required(ErrorMessage = "Task start time is required")]
        public TimeSpan TaskBeginTime { get; set; }
        [Required(ErrorMessage = "Task end time is required")]
        public TimeSpan TaskEndTime { get; set; }
        [Required(ErrorMessage = "Task start date is required")]
        public DateTime TaskBeginDate { get; set; }
        [Required(ErrorMessage = "Task end date is required")]
        public DateTime TaskEndDate { get; set; }
    }
}
