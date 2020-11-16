using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class ManageAccountsViewModel
    {
        public ManageAccountsViewModel(string? username,
            List<Categorization> services, List<Impact> impacts,
            List<Urgency> urgencies, int eId, List<TimeZonesModel> timeZones)
        {
            EmployeeId = eId;
            Services = services;
            Notifier = username;
            Impacts = impacts;
            Urgencies = urgencies;
            TimeZones = timeZones;
        }

        public List<Categorization>? Services { get; set; }
        public string? Notifier { get; set; }
        public int EmployeeId { get; set; }
        public List<Impact> Impacts { get; set; }
        public List<Urgency> Urgencies { get; set; }
        public List<TimeZonesModel> TimeZones { get; set; }
    }
}
