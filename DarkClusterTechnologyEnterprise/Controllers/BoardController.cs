using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        public ViewResult MainBoard()
        {
            return View();
        }
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View("/Home/Index");
        }
    }
}