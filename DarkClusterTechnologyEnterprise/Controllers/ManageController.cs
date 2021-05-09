using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using DarkClusterTechnologyEnterprise.Models.ViewModels;
using DarkClusterTechnologyEnterprise.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class ManageController : Controller
    {
        private readonly IEmployeeRepository repository;
        private readonly PresenceServices _presenceServices;
        private UserManager<IdentityUser> userManager;
        public ManageController(IEmployeeRepository repository,
            UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.repository = repository;
            _presenceServices = new PresenceServices(repository);
        }
        public async Task<IActionResult> PresenceConfirmation(int opt = 0, string category = "work", string clientTimeZone = "")
        {
            
            string name = HttpContext.User.Identity.Name;
            ViewBag.Cat = category;
            ViewBag.Opt = opt;
            PresenceViewModel presenceModel = 
                await _presenceServices.GetPresenceModel(name, opt, category, clientTimeZone);

            return View(presenceModel);
        }

        [HttpPost]
        public async Task<IActionResult> PresenceConfirmation(TimerViewModel timer)
        {
            string name = HttpContext.User.Identity.Name;
            int eId = await repository.GetEmployeeID(name);
            if (timer.begin)
            {
                repository.CreatePresence(eId);
            }
            else if (timer.break_w)
            {
                string presenceId = repository.GetPresences(eId).PresenceId;
                repository.TakeBreak(presenceId);
            }
            else if (timer.stop)
            {
                repository.WorkEnd(eId);
            }

            return RedirectToAction("PresenceConfirmation");
        }

        public IActionResult AcceptanceApplication()
        {

            return View();
        }
        public void Trash()
        {

        }
    }
}


//SQL Queries:

//select DATEDIFF(SECOND, b.BreakStart, b.BreakEnd) AS Seconds
//from Breaks b
//join Presences p
//on b.PresenceId = p.PresenceId
//where (p.EmployeeId = 10 and b.BreakStart > '2020-09-18') order by b.BreakStart;

//SQL TimeZones inserts

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-12:00) International Date Line West',-720);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-11:00) Midway Island, Samoa',-660);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-10:00) Hawaii',-600);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-09:00) Alaska',-540);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-08:00) Pacific Time (US and Canada); Tijuana',-480);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-07:00) Mountain Time (US and Canada)',-420);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-07:00) Chihuahua, La Paz, Mazatlan',-420);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-07:00) Arizona',-420);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-06:00) Central Time (US and Canada)',-360);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-06:00) Saskatchewan',-360);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-06:00) Guadalajara, Mexico City, Monterrey',-360);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-06:00) Central America',-360);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-05:00) Eastern Time (US and Canada)',-300);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-05:00) Indiana (East)',-300);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-05:00) Bogota, Lima, Quito',-300);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-04:00) Atlantic Time (Canada)',-240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-04:00) Georgetown, La Paz, San Juan',-240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-04:00) Santiago',-240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-03:30) Newfoundland',-210);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-03:00) Brasilia',-180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-03:00) Georgetown',-180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-03:00) Greenland',-180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-02:00) Mid-Atlantic',-120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-01:00) Azores',-60);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-01:00) Cape Verde Islands',-60);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT) Greenwich Mean Time: Dublin, Edinburgh, Lisbon, London',0);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT) Monrovia, Reykjavik',0);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague',60);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb',60);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+01:00) Brussels, Copenhagen, Madrid, Paris',60);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna',60);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+01:00) West Central Africa',60);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Minsk',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Cairo',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Helsinki, Kiev, Riga, Sofia, Tallinn, Vilnius',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Athens, Bucharest, Istanbul',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Jerusalem',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Harare, Pretoria',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+03:00) Moscow, St. Petersburg, Volgograd',180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+03:00) Kuwait, Riyadh',180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+03:00) Nairobi',180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+03:00) Baghdad',180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+03:30) Tehran',210);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+04:00) Abu Dhabi, Muscat',240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+04:00) Baku, Tbilisi, Yerevan',240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+04:30) Kabul',270);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+05:00) Ekaterinburg',300);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+05:00) Tashkent',300);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi',330);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+05:45) Kathmandu',345);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+06:00) Astana, Dhaka',360);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+06:00) Sri Jayawardenepura',360);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+06:00) Almaty, Novosibirsk',360);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+06:30) Yangon (Rangoon)',390);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+07:00) Bangkok, Hanoi, Jakarta',420);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+07:00) Krasnoyarsk',420);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi',480);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+08:00) Kuala Lumpur, Singapore',480);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+08:00) Taipei',480);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+08:00) Perth',480);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+08:00) Irkutsk, Ulaanbaatar',480);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+09:00) Seoul',540);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+09:00) Osaka, Sapporo, Tokyo',540);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+09:00) Yakutsk',540);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+09:30) Darwin',570);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+09:30) Adelaide',570);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+10:00) Canberra, Melbourne, Sydney',600);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+10:00) Brisbane',600);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+10:00) Hobart',600);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+10:00) Vladivostok',600);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+10:00) Guam, Port Moresby',600);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+11:00) Magadan, Solomon Islands, New Caledonia',660);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+12:00) Fiji, Kamchatka, Marshall Is.',720);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+12:00) Auckland, Wellington',720);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+13:00) Nuku`alofa',780);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Beirut',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Amman',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-08:00) Tijuana, Baja California',-480);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+02:00) Windhoek',120);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+03:00) Tbilisi',180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-04:00) Manaus',-240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-03:00) Montevideo',-180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+04:00) Yerevan',240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-04:30) Caracas',-270);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-03:00) Buenos Aires',-180);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT) Casablanca',0);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+05:00) Islamabad, Karachi',300);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+04:00) Port Louis',240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT-04:00) Asuncion',-240);

//insert into Zone(TimeZoneName, TimeDifference)
//Values('(GMT+12:00) Petropavlovsk-Kamchatsky',720);