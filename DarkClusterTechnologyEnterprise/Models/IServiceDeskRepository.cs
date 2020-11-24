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
        List<Categorization> GetServices(string category);
        List<Impact> GetImpacts();
        List<Urgency> GetUrgencies();
        int SetPriority(int urgency, int impact);
        Task<bool> CreateNewServiceRequest(ReceiveServiceRequest receive);
        Task CreateNewAccountForm(ReceiveNewAccountForm account, ReceiveNewTaskRequest newTask);
        Task CreateCloseAccountTask(ReceiveNewTaskRequest newTask);
        Task CreateNewIncident(RecivedIncident report);
    }
}
