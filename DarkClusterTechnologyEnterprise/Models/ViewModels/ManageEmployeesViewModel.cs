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
    public class PositionSearchResult
    {
        public PositionSearchResult() { }
        public PositionSearchResult(List<Position> positions)
        {
            Results = new List<PositionSearchResult>();
            foreach (var p in positions)
            {
                Results.Add(new PositionSearchResult
                {
                    FullInfo = p.PositionName,
                    Id = p.PositionId
                });
            }

        }
        public string? FullInfo { get; set; }
        public int Id { get; set; }
        public List<PositionSearchResult> Results { get; set; }
    }
    public class DepartmentSearchResult
    {
        public DepartmentSearchResult() { }
        public DepartmentSearchResult(List<Department> departments)
        {
            Results = new List<DepartmentSearchResult>();
            foreach (var d in departments)
            {
                Results.Add(new DepartmentSearchResult
                {
                    FullInfo = d.DepartmentName,
                    Id = d.DepartmentId
                });
            }

        }
        public string? FullInfo { get; set; }
        public int Id { get; set; }
        public List<DepartmentSearchResult> Results { get; set; }
    }
}
