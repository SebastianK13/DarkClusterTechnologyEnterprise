﻿using DarkClusterTechnologyEnterprise.Models.ViewModels;
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
        public List<Categorization> GetServices() =>
            context.Categorizations
            .Where(t => t.Category.CategoryName == "Service request")
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

            return true;
        }
        //private async Task<Status> CreateStatus()
        //{

        //}

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
