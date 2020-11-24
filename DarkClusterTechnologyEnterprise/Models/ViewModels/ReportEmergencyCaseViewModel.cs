using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class IncidentViewModel
    {
        public IncidentViewModel(string? username,
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
    public class RecivedIncident
    {
        [Required]
        public int Services { get; set; }
        [Required]
        public int Urgencies { get; set; }
        [Required]
        public int Impacts { get; set; }
        [Required]
        public int RequestedBy { get; set; }
        [Required]
        public int ContactPerson { get; set; }
        [Required]
        public string? Topic { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
