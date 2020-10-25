using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult ManageEmployeesAccount()
        {

            return View();
        }       
        public IActionResult MakeOrder()
        {

            return View();
        }        
        public IActionResult ScheduleServiceWorks()
        {

            return View();
        }        
        public IActionResult ReportEmergencyCase()
        {

            return View();
        }
    }
}