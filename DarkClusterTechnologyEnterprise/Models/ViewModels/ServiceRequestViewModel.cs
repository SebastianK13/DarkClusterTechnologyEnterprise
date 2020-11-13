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
    public class SearchResult
    {
        public SearchResult() { }
        public SearchResult(List<Employee> employees)
        {
            Results = new List<SearchResult>();
            foreach (var e in employees)
            {
                Results.Add(new SearchResult
                {
                    FullInfo = e.Firstname + ' ' + e.Surname +", "+ e.Location.City,
                    EmployeeId = e.EmployeeId
                });
            }

        }
        //firstname + surname+localization
        public string? FullInfo { get; set; }
        public int EmployeeId { get; set; }
        public List<SearchResult> Results { get; set; }
    }
}
