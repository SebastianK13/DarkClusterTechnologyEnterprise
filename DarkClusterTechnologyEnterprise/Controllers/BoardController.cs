using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using DarkClusterTechnologyEnterprise.Models.ViewModels;
using DarkClusterTechnologyEnterprise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly PresenceServices _presenceServices;
        public BoardController(IEmployeeRepository repository)
        {
            _presenceServices = new PresenceServices(repository);
            _repository = repository;
        }

        public async Task<IActionResult> MainBoard()
        {
            List<PresenceViewModel> presences = new List<PresenceViewModel>();
            string name = HttpContext.User.Identity.Name;
            presences.Add(await _presenceServices.GetPresenceModel(name, 2));
            presences.Add(await _presenceServices.GetPresenceModel(name));
            presences.Add(await _presenceServices.GetPresenceModel(name, 1));
            return View(presences);
        }
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View("/Home/Index");
        }
    }
}