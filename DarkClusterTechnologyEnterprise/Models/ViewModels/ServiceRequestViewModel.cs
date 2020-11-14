using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class ServiceRequestViewModel
    {
        public ServiceRequestViewModel(string? username,
            List<Categorization> services, List<Impact> impacts,
            List<Urgency> urgencies, int eId)
        {
            EmployeeId = eId;
            Services = services;
            Notifier = username;
            Impacts = impacts;
            Urgencies = urgencies;
        }

        public List<Categorization>? Services { get; set; }
        public string? Notifier { get; set; }
        public int EmployeeId { get; set; }
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
    public class ReceiveServiceRequest
    { 
        public int Services { get; set; }
        public int Urgencies { get; set; }
        public int Impacts { get; set; }
        public int RequestedBy { get; set; }
        public int ContactPerson { get; set; }
        public string? Topic { get; set; }
        public string? Description { get; set; }
    }

}
