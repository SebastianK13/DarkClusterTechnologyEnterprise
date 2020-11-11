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
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int ServiceWorkId { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? ResponsibleEmployee { get; set; }
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
