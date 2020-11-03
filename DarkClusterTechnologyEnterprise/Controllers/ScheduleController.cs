using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult TaskSchedule()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewTask()
        {
            return RedirectToAction("TaskSchedule");
        }
    }
}