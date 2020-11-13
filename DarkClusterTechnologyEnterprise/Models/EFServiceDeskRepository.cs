using DarkClusterTechnologyEnterprise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DarkClusterTechnologyEnterprise.Models.ServiceDeskModels;

namespace DarkClusterTechnologyEnterprise.Models
{
    public class EFServiceDeskRepository: IServiceDeskRepository
    {
        AppServiceDeskDbContext context;
        public EFServiceDeskRepository(AppServiceDeskDbContext context)
        {
            this.context = context;
        }
        public List<ScheduledWork> GetServiceWorks(int eId, DateTime date) => 
            context.Works
            .Where(d => d.BeginDate.Date >= date.Date)
            .ToList();

        public async Task<bool> CreateServiceWork(NewServiceWork newService, int eId)
        {
            ScheduledWork scheduledWork = new ScheduledWork(newService, eId);
            context.Works.Add(scheduledWork);
            return await context.SaveChangesAsync() > 0;
        }
        public List<ServiceNotification> GetServices() =>
            context.Services
            .Where(t => t.NotificationType == "Service request")
            .ToList();

        public List<Impact> GetImpacts() =>
            context.Impacts.ToList();
        public List<Urgency> GetUrgencies() =>
            context.Urgencies.ToList();
    }
}
