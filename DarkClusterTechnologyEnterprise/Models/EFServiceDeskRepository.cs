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

    }
}
