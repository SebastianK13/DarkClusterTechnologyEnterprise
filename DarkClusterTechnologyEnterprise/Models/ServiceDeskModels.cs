using DarkClusterTechnologyEnterprise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models
{
    public class ServiceDeskModels
    {
        public class UserApplication
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int ApplicationId { get; set; }
            public string Topic { get; set; }
            public string Description { get; set; }
            //User who made an application
            public string Applicant { get; set; }
            public string UserLocation { get; set; }
            public string Priority { get; set; }
            public bool Critical { get; set; }
            //Administrator responsible for this application
            public string Responsible { get; set; }
            public string ApplicationStatus { get; set; }
        }
        public class ApplicationConversation
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public string User { get; set; }
            public string Administrator { get; set; }
            public string Message { get; set; }
            public DateTime MessageDate { get; set; }
            [ForeignKey("UserApplication")]
            public int ApplicationId { get; set; }
            public UserApplication UserApplication { get; set; }
        }
        public class ScheduledWork
        {
            public ScheduledWork() { }
            public ScheduledWork(NewServiceWork newService, int eId)
            {
                Name = newService.ServiceWork;
                Description = newService.ServiceWorkDesc;
                BeginDate = newService.ServiceBeginDate + newService.ServiceBeginTime;
                EndDate = newService.ServiceEndDate + newService.ServiceEndTime;
                ResponsibleEmployee = eId;
            }
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int ServiceWorkId { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public int ResponsibleEmployee { get; set; }
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }
        }
        public class ServiceNotification
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int Id { get; set; }
            //Incident or Application
            public string? NotificationType { get; set; }
            public string? ServiceName { get; set; }
        }
        public class Impact
        {
            public int Id { get; set; }
            //max 4 lvl
            public int level { get; set; }
            public string? Name { get; set; }
        }
        public class Urgency
        {
            public int Id { get; set; }
            //max 3 lvl
            public int level { get; set; }
            public string? Name { get; set; }
        }
    }
}
