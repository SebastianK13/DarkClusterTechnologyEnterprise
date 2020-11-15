using DarkClusterTechnologyEnterprise.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<Categorization> GetServices(string category) =>
            context.Categorizations
            .Where(t => t.Category.CategoryName == category)
            .ToList();

        public List<Impact> GetImpacts() =>
            context.Impacts.ToList();
        public List<Urgency> GetUrgencies() =>
            context.Urgencies.ToList();
        public int GetImpactLvlById(int impactId) =>
            context.Impacts
            .Where(i => i.Id == impactId)
            .Select(l=>l.level)
            .FirstOrDefault();
        public int GetUrgencyLvlById(int urgencyId) =>
            context.Urgencies
            .Where(i => i.Id == urgencyId)
            .Select(l => l.level)
            .FirstOrDefault();
        public async Task<bool> CreateNewServiceRequest(ReceiveServiceRequest receive)
        {
            ServiceRequest request = new ServiceRequest(receive);
            request.PriorityId = SetPriority(
                GetUrgencyLvlById(receive.Urgencies),
                GetImpactLvlById(receive.Impacts));
            request.CategoryId = receive.Services;
            request.Status = await CreateStatus();

            context.Applications.Add(request);

            return await context.SaveChangesAsync() > 0; 
        }
        private async Task<Status> CreateStatus()
        {
            Status status = new Status();
            status.StateId = await context.States
                .Where(n => n.StateName == "New")
                .Select(i => i.StateId)
                .FirstOrDefaultAsync();
            status.CreateDate = DateTime.UtcNow;
            status.Expired = false;
            status.DueTime = DateTime.UtcNow.AddDays(2);
            status.StateId = await FindStateId("New");

            context.Statuses.Add(status);
            await context.SaveChangesAsync();

            return status;
        }
        private async Task<int> FindStateId(string stateName) =>
            await context.States
            .Where(n => n.StateName == stateName)
            .Select(i => i.StateId).FirstOrDefaultAsync();
        public int SetPriority(int urgency, int impact)
        {
            switch (urgency)
            {
                case 1:
                    switch (impact)
                    {
                        case int i when (i<=2):
                            return FindPriorityIdByLvl(1);
                        case int i when (i>3):
                            return FindPriorityIdByLvl(2);
                    }
                    break;
                case 2:
                    switch (impact)
                    {
                        case 1:
                            return FindPriorityIdByLvl(1);
                        case int i when(i ==2 || i == 3):
                            return FindPriorityIdByLvl(2);
                        case 4:
                            return FindPriorityIdByLvl(3);
                    }
                    break;
                case 3:
                    switch (impact)
                    {
                        case 1:
                            return FindPriorityIdByLvl(2);
                        case int i when(i>1):
                            return FindPriorityIdByLvl(3);
                    }
                    break;
                case 4:
                    return FindPriorityIdByLvl(4);
            }
            return 0;
        }
        private int FindPriorityIdByLvl(int level) =>
            context.Priorities
                .Where(l => l.level == level)
                .Select(i=>i.Id)
                .FirstOrDefault();
    }
}
