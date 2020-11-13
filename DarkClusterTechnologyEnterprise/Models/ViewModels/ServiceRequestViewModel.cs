using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DarkClusterTechnologyEnterprise.Models.ServiceDeskModels;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class ServiceRequestViewModel
    {
        public ServiceRequestViewModel(string? username,
            List<ServiceNotification> services, List<Impact> impacts,
            List<Urgency> urgencies)
        {
            Services = services;
            Notifier = username;
            Impacts = impacts;
            Urgencies = urgencies;
        }

        public List<ServiceNotification>? Services { get; set; }
        public string? Notifier { get; set; }
        public List<Impact> Impacts { get; set; }
        public List<Urgency> Urgencies { get; set; }
    }
}
