using DarkClusterTechnologyEnterprise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models
{
    public interface IServiceDeskRepository
    {
        List<ScheduledWork> GetServiceWorks(int eId, DateTime date);
        Task<bool> CreateServiceWork(NewServiceWork newService, int eId);
        List<Categorization> GetServices();
        List<Impact> GetImpacts();
        List<Urgency> GetUrgencies();
    }
}
