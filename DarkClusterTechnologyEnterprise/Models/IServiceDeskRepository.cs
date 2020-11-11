using DarkClusterTechnologyEnterprise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DarkClusterTechnologyEnterprise.Models.ServiceDeskModels;

namespace DarkClusterTechnologyEnterprise.Models
{
    public interface IServiceDeskRepository
    {
        List<ScheduledWork> GetServiceWorks(int eId, DateTime date);
        Task<bool> CreateServiceWork(NewServiceWork newService, int eId);
    }
}
