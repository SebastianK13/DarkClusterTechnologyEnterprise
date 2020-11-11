using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using DarkClusterTechnologyEnterprise.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class ApplicationController : Controller
    {
        IServiceDeskRepository sdRepository;
        IEmployeeRepository eRepository;
        public ApplicationController(IServiceDeskRepository sdRepo, IEmployeeRepository eRepo)
        {
            sdRepository = sdRepo;
            eRepository = eRepo;
        }
        List<DateTime> month = new List<DateTime>();
        private void createMonth(ref List<DateTime> month, int eId)
        {
            for (int i = 0; i <= 30; i++)
            {
                month.Add(eRepository.ConvertToLocal(DateTime.UtcNow.AddDays(+i), eId));
            }
        }
        public IActionResult ManageEmployeesAccount()
        {

            return View();
        }       
        public IActionResult MakeOrder()
        {

            return View();
        }        
        public async Task<IActionResult> ScheduleServiceWorks()
        {
            string? username = User.Identity.Name;
            int eId = await eRepository.GetEmployeeID(username);
            createMonth(ref month, eId);
            DateTime date = eRepository.ConvertToLocal(DateTime.UtcNow, eId);
            ServiceWork serviceWork = new ServiceWork(sdRepository.GetServiceWorks(eId, date));
            var serviceWorks = SplitServiceWorks(serviceWork);

            ServiceWorksViewModel model = new ServiceWorksViewModel(serviceWorks, month);

            return View(model);
        }        
        public IActionResult ReportEmergencyCase()
        {

            return View();
        }
        private List<ServiceWork> SplitServiceWorks(ServiceWork serviceWorks)
        {
            List<ServiceWork> worksSplit = new List<ServiceWork>();
            foreach (var w in serviceWorks.ServiceWorks)
            {
                double numOfDays = (w.EndDate.Date - w.BeginDate.Date).TotalDays;
                if (numOfDays <= 1)
                {
                    if (w.BeginDate.Date == w.EndDate.Date)
                    {
                        worksSplit.Add(w);
                    }
                    else
                    {
                        DateTime midnight = w.BeginDate.Date.AddDays(1).AddSeconds(-1);
                        DateTime nextDay = w.EndDate.Date.AddDays(1);

                        worksSplit.Add(new ServiceWork
                        {
                            Name = w.Name,
                            Description = w.Description,
                            BeginDate = w.BeginDate,
                            EndDate = midnight,
                            Id = w.Id,
                            ResponsibleEmployee = w.ResponsibleEmployee
                        });

                        worksSplit.Add(new ServiceWork
                        {
                            Name = w.Name,
                            Description = w.Description,
                            BeginDate = nextDay,
                            EndDate = w.EndDate,
                            Id = w.Id,
                            ResponsibleEmployee = w.ResponsibleEmployee
                        });
                    }

                }
            }
            worksSplit.Sort((x, y) => DateTime.Compare(x.BeginDate, y.BeginDate));

            return worksSplit;
        }
    }
}