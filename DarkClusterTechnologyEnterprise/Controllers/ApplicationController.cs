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
        public async Task<IActionResult> ManageEmployeesAccount()
        {
            string? username = User.Identity.Name;
            int eId = await eRepository.GetEmployeeID(username);
            ManageAccountsViewModel viewModel =
                new ManageAccountsViewModel(username, sdRepository.GetServices("Task"),
                sdRepository.GetImpacts(), sdRepository.GetUrgencies(), eId,
                eRepository.GetTimeZones());

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewTaskRequest(ReceiveNewTaskRequest newTask, ReceiveNewAccountForm account)
        {

            return RedirectToAction("ManageEmployeesAccount");
        }
        public IActionResult FindPosition(string phrase)
        {
            PositionSearchResult viewModel = new PositionSearchResult(eRepository.FindPositionByPhrase(phrase));

            return Ok(viewModel.Results);
        }
        public IActionResult FindDepartment(string phrase)
        {
            DepartmentSearchResult viewModel = new DepartmentSearchResult(eRepository.FindDepartmentByPhrase(phrase));

            return Ok(viewModel.Results);
        }
        public async Task<IActionResult> ServiceRequest()
        {
            string? username = User.Identity.Name;
            int eId = await eRepository.GetEmployeeID(username);
            ServiceRequestViewModel viewModel =
                new ServiceRequestViewModel(username, sdRepository.GetServices("Service request"),
                sdRepository.GetImpacts(), sdRepository.GetUrgencies(), eId);

            return View(viewModel);
        }
        public IActionResult FindEmployee(string phrase)
        {
            SearchResult viewModel = new SearchResult(eRepository.FindEmployeeByPhrase(phrase));

            return Ok(viewModel.Results);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewServiceRequest(ReceiveServiceRequest receive)
        {
            if (ModelState.IsValid)
            {
                await sdRepository.CreateNewServiceRequest(receive);
            }

            return RedirectToAction("ServiceRequest");
        }
        public async Task<IActionResult> ScheduleServiceWorks()
        {
            string? username = User.Identity.Name;
            int eId = await eRepository.GetEmployeeID(username);
            createMonth(ref month, eId);
            DateTime date = eRepository.ConvertToLocal(DateTime.UtcNow, eId);
            var serviceWork = SetServiceWorksViewModel(eId, date);
            var serviceWorks = SplitServiceWorks(serviceWork);

            ServiceWorksViewModel model = new ServiceWorksViewModel(serviceWorks, month);

            return View(model);
        }
        private List<ServiceWork> SetServiceWorksViewModel(int eId, DateTime date)
        {
            List<ServiceWork> serviceWorks = new List<ServiceWork>();
            var scheduledWorks = sdRepository.GetServiceWorks(eId, date);
            foreach (var w in scheduledWorks)
            {
                serviceWorks.Add(new ServiceWork()
                {
                    Name = w.Name,
                    Id = w.ServiceWorkId,
                    Description = w.Description,
                    BeginDate = w.BeginDate,
                    EndDate = w.EndDate,
                    ResponsibleEmployee = eRepository.GetNameSurname(w.ResponsibleEmployee) 
                });
            }
            return serviceWorks;
        }
        [HttpPost]
        public async Task<IActionResult> NewServiceWork(NewServiceWork newService)
        {
            if (ModelState.IsValid)
            {
                string? username = User.Identity.Name;
                int eId = await eRepository.GetEmployeeID(username);
                await sdRepository.CreateServiceWork(newService, eId);
            }

            return RedirectToAction("ScheduleServiceWorks");
        }
        public IActionResult ReportEmergencyCase()
        {

            return View();
        }
        private List<ServiceWork> SplitServiceWorks(List<ServiceWork> serviceWorks)
        {
            List<ServiceWork> worksSplit = new List<ServiceWork>();
            foreach (var w in serviceWorks)
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
                        DateTime nextDay = w.BeginDate.Date.AddDays(1);

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