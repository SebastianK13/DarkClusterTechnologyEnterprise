﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class NewServiceWork
    {
        [Required(ErrorMessage = "Name is required")]
        public string? ServiceWork { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? ServiceWorkDesc { get; set; }
        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan ServiceBeginTime { get; set; }
        [Required(ErrorMessage = "End time is required")]
        public TimeSpan ServiceEndTime { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime ServiceBeginDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public DateTime ServiceEndDate { get; set; }
    }
    public class ServiceWorksViewModel
    {
        public ServiceWorksViewModel(List<ServiceWork> serviceWorks, List<DateTime> dates)
        {
            ServiceWorks = serviceWorks;
            Dates = dates;
        }
        public List<ServiceWork>? ServiceWorks { get; set; }
        public List<DateTime>? Dates { get; set; }
    }
    public class ServiceWork
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ResponsibleEmployee { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
