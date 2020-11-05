using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkClusterTechnologyEnterprise.Models;
using DarkClusterTechnologyEnterprise.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    public class ManageController : Controller
    {
        private IEmployeeRepository repository;
        private UserManager<IdentityUser> userManager;
        public ManageController(IEmployeeRepository repository,
            UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.repository = repository;
        }
        List<DateTime> week = new List<DateTime>
            {
                 DateTime.Now.ToUniversalTime().AddDays(-8).Date + new TimeSpan(0,0,0),
                 DateTime.Now.ToUniversalTime().AddDays(-7).Date + new TimeSpan(0,0,0),
                 DateTime.Now.ToUniversalTime().AddDays(-6).Date + new TimeSpan(0,0,0),
                 DateTime.Now.ToUniversalTime().AddDays(-5).Date + new TimeSpan(0,0,0),
                 DateTime.Now.ToUniversalTime().AddDays(-4).Date + new TimeSpan(0,0,0),
                 DateTime.Now.ToUniversalTime().AddDays(-3).Date + new TimeSpan(0,0,0),
                 DateTime.Now.ToUniversalTime().AddDays(-2).Date + new TimeSpan(0,0,0),
                 DateTime.Now.ToUniversalTime().AddDays(-1).Date + new TimeSpan(0,0,0)
            };
        List<DateTime> month = new List<DateTime>();
        public void createMonth(ref List<DateTime> month)
        {
            for (int i = 1; i <= 31; i++) 
            {
                month.Add(DateTime.Now.ToUniversalTime().AddDays(-i).Date + new TimeSpan(0, 0, 0));
            }
        }
        public async Task<IActionResult> PresenceConfirmation(int opt = 0, string category = "work", string clientTimeZone = "")
        {
            
            string? name = HttpContext.User.Identity.Name;
            int eId = await repository.GetEmployeeID(name);
            Presence model = repository.GetPresences(eId);
            PresenceViewModel presenceModel = new PresenceViewModel();
            var allPresences = repository.GetAllPresences(eId);
            var allBreaks = repository.GetAllBreaks(allPresences.Select(x=>x.PresenceId).ToList());
            DateTime dateCriteria = DateTime.Now.ToUniversalTime().AddDays(-7);
            createMonth(ref month);
            ViewBag.Cat = category;
            ViewBag.Opt = opt;
            switch (opt)
            {
                case 1:
                    dateCriteria = DateTime.Now.ToUniversalTime().AddDays(-31);
                    presenceModel.Presences = allPresences.Where(b => b.WorkBeginTime.Date >= dateCriteria.Date).OrderBy(b => b.WorkBeginTime).ToList();
                    presenceModel.DataModel = WeeklyStats(presenceModel.Presences);
                    presenceModel.Breaks = allBreaks.Where(b => b.BreakStart.Date >= dateCriteria.Date).OrderBy(b => b.BreakStart).ToList();
                    fillRange(ref presenceModel, month);
                    //todo: fillRange for Avgs 
                    presenceModel.AvgPresences = AvgPresences(repository.GetPresenceAllUsers(dateCriteria), month);
                    InsertBreaksToModel(ref presenceModel);
                    presenceModel.AvgBreaks = CreateAvgBreaks(repository.GetBreaksAllUsers(dateCriteria), month);
                    presenceModel.AvgPresences = AvgPresences(repository.GetPresenceAllUsers(dateCriteria), month);
                    fillRangeWTAvg(ref presenceModel, month);
                    fillRangeBreakAvg(ref presenceModel, month);
                    break;
                case 2:
                    dateCriteria = DateTime.Now.ToUniversalTime().AddDays(-2);
                    presenceModel.Presences = allPresences.Where(b => b.WorkBeginTime.Date >= dateCriteria.Date).OrderBy(b => b.WorkBeginTime).ToList();
                    presenceModel.DataModel = WeeklyStats(presenceModel.Presences);
                    presenceModel.Breaks = allBreaks.Where(b => b.BreakStart.Date >= dateCriteria.Date).OrderBy(b => b.BreakStart).ToList();
                    List<DateTime> datesRange = new List<DateTime>
                    {
                        DateTime.Now.ToUniversalTime().AddDays(-2),
                        DateTime.Now.ToUniversalTime().AddDays(-1)
                    };
                    fillRange(ref presenceModel, datesRange);
                    InsertBreaksToModel(ref presenceModel);
                    presenceModel.AvgBreaks = CreateAvgBreaks(repository.GetBreaksAllUsers(dateCriteria), datesRange);
                    presenceModel.AvgPresences = AvgPresences(repository.GetPresenceAllUsers(dateCriteria), datesRange);
                    fillRangeWTAvg(ref presenceModel, datesRange);
                    fillRangeBreakAvg(ref presenceModel, datesRange);

                    break;
                //case 3:
                //    dateCriteria = DateTime.Now;
                //    presenceModel.Presences = allPresences.Where(b => b.WorkBeginTime.Date >= dateCriteria.Date).OrderBy(b => b.WorkBeginTime).ToList();
                //    presenceModel.DataModel = WeeklyStats(presenceModel.Presences);
                //    //fillRange(ref presenceModel);
                //    presenceModel.AvgPresences = AvgPresences(repository.GetPresenceAllUsers(), week);
                //    break;
                default:
                    presenceModel.Presences = allPresences.Where(b => b.WorkBeginTime.Date >= dateCriteria.Date).OrderBy(b => b.WorkBeginTime).ToList();
                    presenceModel.DataModel = WeeklyStats(presenceModel.Presences);
                    presenceModel.Breaks = allBreaks.Where(b => b.BreakStart.Date >= dateCriteria.Date).OrderBy(b => b.BreakStart).ToList();
                    fillRange(ref presenceModel, week);
                    InsertBreaksToModel(ref presenceModel);
                    presenceModel.AvgBreaks = CreateAvgBreaks(repository.GetBreaksAllUsers(dateCriteria), week);
                    presenceModel.AvgPresences = AvgPresences(repository.GetPresenceAllUsers(dateCriteria), week);
                    fillRangeWTAvg(ref presenceModel, week);
                    fillRangeBreakAvg(ref presenceModel, week);
                    break;
            }
            if (model != null)
            {
                TimeSpan current = repository.CheckIfDaylight(eId) - model.WorkBeginTime;
                repository.RoundToSeconds(ref current);
                presenceModel.BreakActive = repository.FindActiveBreak(eId);
                double seconds = current.TotalSeconds;
                presenceModel.Seconds = seconds;
                presenceModel.BreakLength = repository.GetActiveBreakTime(eId);
            }
            else
            {
                presenceModel.Seconds = -1;
            }

            return View(presenceModel);
        }
        private void InsertBreaksToModel(ref PresenceViewModel pVM)
        {
            foreach (var p in pVM.Breaks)
            {
                double numOfDays = (p.BreakEnd.Date - p.BreakStart.Date).TotalDays;
                if (numOfDays <= 1)
                {
                    int result = p.BreakStart.Date.CompareTo(p.BreakEnd.Date);
                    var existForEqual = pVM.DataModel.Where(b => b.BreakStart.Date == p.BreakStart.Date).FirstOrDefault();
                    var existForGreater = pVM.DataModel.Where(b => b.BreakStart.Date == p.BreakEnd.Date).FirstOrDefault();

                        switch (result)
                    {
                        case 0:
                            if (existForEqual != null)
                            {
                                foreach (var e in pVM.DataModel.Where(b => b.WorkBeginTime.Date == p.BreakStart.Date))
                                {
                                    e.BreakTime += (p.BreakEnd.TimeOfDay - p.BreakStart.TimeOfDay).TotalSeconds;
                                }
                            }
                            else
                            {
                                foreach (var w in pVM.DataModel.Where(y=>y.WorkBeginTime.Date == p.BreakStart.Date))
                                {
                                    w.BreakStart = p.BreakStart;
                                    w.BreakTime += (p.BreakEnd.TimeOfDay - p.BreakStart.TimeOfDay).TotalSeconds;
                                }

                            }
                            break;
                        case -1:

                            if (existForEqual != null)
                            {
                                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                                TimeSpan ts2 = new TimeSpan(p.BreakStart.Hour, p.BreakStart.Minute, p.BreakStart.Second);
                                var totalTime = ts1 - ts2;
                                repository.RoundToSeconds(ref totalTime);
                                foreach (var e in pVM.DataModel.Where(b => b.WorkBeginTime.Date == p.BreakStart.Date))
                                {
                                    e.BreakTime += totalTime.TotalSeconds;
                                }
                            }
                            else
                            {
                                foreach (var w in pVM.DataModel.Where(y => y.WorkBeginTime.Date == p.BreakStart.Date))
                                {
                                    TimeSpan ts1 = new TimeSpan(24, 0, 0);
                                    TimeSpan ts2 = new TimeSpan(p.BreakStart.Hour, p.BreakStart.Minute, p.BreakStart.Second);
                                    var totalTime = ts1 - ts2;
                                    repository.RoundToSeconds(ref totalTime);
                                    DateTime dt = p.BreakStart;
                                    w.BreakTime = totalTime.TotalSeconds;
                                    w.BreakStart = dt;
                                }
                            }

                            if (existForGreater != null)
                            {
                                TimeSpan ts2 = new TimeSpan(p.BreakEnd.Hour, p.BreakEnd.Minute, p.BreakEnd.Second);
                                repository.RoundToSeconds(ref ts2);
                                foreach (var e in pVM.DataModel.Where(b => b.WorkBeginTime.Date == p.BreakEnd.Date))
                                {
                                    e.BreakTime += ts2.TotalSeconds;
                                }
                            }
                            else
                            {
                                foreach (var w in pVM.DataModel.Where(y => y.WorkBeginTime.Date == p.BreakEnd.Date))
                                {
                                    TimeSpan ts2 = new TimeSpan(p.BreakEnd.Hour, p.BreakEnd.Minute, p.BreakEnd.Second);
                                    repository.RoundToSeconds(ref ts2);
                                    w.BreakTime += ts2.TotalSeconds;
                                    w.BreakStart = p.BreakEnd.Date + new TimeSpan(0, 0, 0);
                                }

                            }
                            break;
                    }
                }

            }
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
        public List<GraphViewModel> WeeklyStats(List<Presence> presences)
        {
            List<GraphViewModel> model = new List<GraphViewModel>();

            foreach (var p in presences)
            {
                //if ((p.WorkBeginTime.Date == DateTime.Now.ToUniversalTime().AddDays(-19).Date) && p.EmployeeId == 10)
                //{
                //    //
                //}

                double numOfDays = (p.WorkEndTime.Date - p.WorkBeginTime.Date).TotalDays;
                if (numOfDays <= 1)
                {
                    int result = p.WorkBeginTime.Date.CompareTo(p.WorkEndTime.Date);
                    GraphViewModel presence = new GraphViewModel();
                    var existForEqual = model.Where(b => b.WorkBeginTime.Date == p.WorkBeginTime.Date).FirstOrDefault();
                    var existForGreater = model.Where(b => b.WorkBeginTime.Date == p.WorkEndTime.Date).FirstOrDefault();

                    switch (result)
                    {
                        case 0:
                            if (existForEqual != null)
                            {
                                foreach (var e in model.Where(b => b.WorkBeginTime.Date == p.WorkBeginTime.Date))
                                {
                                    e.WorkTime += (p.WorkEndTime.TimeOfDay - p.WorkBeginTime.TimeOfDay).TotalSeconds;
                                }
                            }
                            else
                            {
                                presence.WorkBeginTime = p.WorkBeginTime;
                                presence.WorkTime += (p.WorkEndTime.TimeOfDay - p.WorkBeginTime.TimeOfDay).TotalSeconds;
                                model.Add(presence);
                            }
                            break;
                        case -1:

                            if (existForEqual != null)
                            {
                                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                                TimeSpan ts2 = new TimeSpan(p.WorkBeginTime.Hour, p.WorkBeginTime.Minute, p.WorkBeginTime.Second);
                                var totalTime = ts1 - ts2;
                                repository.RoundToSeconds(ref totalTime);
                                foreach (var e in model.Where(b => b.WorkBeginTime.Date == p.WorkBeginTime.Date))
                                {
                                    e.WorkTime += totalTime.TotalSeconds;
                                }
                            }
                            else
                            {
                                GraphViewModel presenceForEqual = new GraphViewModel();
                                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                                TimeSpan ts2 = new TimeSpan(p.WorkBeginTime.Hour, p.WorkBeginTime.Minute, p.WorkBeginTime.Second);
                                var totalTime = ts1 - ts2;
                                repository.RoundToSeconds(ref totalTime);
                                DateTime dt = p.WorkBeginTime;
                                presenceForEqual.WorkTime = totalTime.TotalSeconds;
                                presenceForEqual.WorkBeginTime = dt;
                                model.Add(presenceForEqual);
                            }

                            if (existForGreater != null)
                            {
                                TimeSpan ts2 = new TimeSpan(p.WorkEndTime.Hour, p.WorkEndTime.Minute, p.WorkEndTime.Second);
                                repository.RoundToSeconds(ref ts2);
                                foreach (var e in model.Where(b => b.WorkBeginTime.Date == p.WorkEndTime.Date))
                                {
                                    e.WorkTime += ts2.TotalSeconds;
                                }
                            }
                            else
                            {
                                TimeSpan ts2 = new TimeSpan(p.WorkEndTime.Hour, p.WorkEndTime.Minute, p.WorkEndTime.Second);
                                repository.RoundToSeconds(ref ts2);
                                presence.WorkTime += ts2.TotalSeconds;
                                presence.WorkBeginTime = p.WorkEndTime.Date + new TimeSpan(0, 0, 0);
                                model.Add(presence);
                            }
                            break;
                    }
                }

            }
            return model;
        }
        public void fillRange(ref PresenceViewModel presenceModel, List<DateTime> dates)
        {
            foreach (var d in dates)
            {
                if (!presenceModel.DataModel.Any(x => x.WorkBeginTime.Date == d.Date))
                {
                    presenceModel.DataModel.Add(new GraphViewModel { WorkBeginTime = d.Date, WorkTime = 0 });
                }
            }

            presenceModel.DataModel.Sort((x, y) => DateTime.Compare(x.WorkBeginTime, y.WorkBeginTime));
        }
        public void fillRangeWTAvg(ref PresenceViewModel presenceModel, List<DateTime> dates)
        {
            foreach (var d in dates)
            {
                if (!presenceModel.AvgPresences.Any(x => x.DayOfWork.Date == d.Date))
                {
                    presenceModel.AvgPresences.Add(new AvgPresence { DayOfWork = d.Date, AvgTime = 0 });
                }
            }

            presenceModel.AvgPresences.Sort((x, y) => DateTime.Compare(x.DayOfWork, y.DayOfWork));
        }
        //public void fillRangeForBrake(ref PresenceViewModel presenceModel, List<DateTime> dates)
        //{
        //    foreach (var d in dates)
        //    {
        //        if (!presenceModel.DataModel.Any(x => x.WorkBeginTime.Date == d.Date))
        //        {
        //            presenceModel.DataModel.Add(new GraphViewModel { WorkBeginTime = d.Date, WorkTime = 0 });
        //        }
        //    }

        //    presenceModel.DataModel.Sort((x, y) => DateTime.Compare(x.WorkBeginTime, y.WorkBeginTime));
        //}
        public void fillRangeBreakAvg(ref PresenceViewModel presenceModel, List<DateTime> dates)
        {
            foreach (var d in dates)
            {
                if (!presenceModel.AvgBreaks.Any(x => x.DayOfWork.Date == d.Date))
                {
                    presenceModel.AvgBreaks.Add(new AvgBreaks { DayOfWork = d.Date, AvgBreakTime = 0 });
                }
            }

            presenceModel.DataModel.Sort((x, y) => DateTime.Compare(x.WorkBeginTime, y.WorkBeginTime));
        }
        public List<AvgPresence> AvgPresences(List<Presence> presences, List<DateTime> dates)
        {
            List<AvgPresence> model = new List<AvgPresence>();
            List<EmployeeAmount> employees = new List<EmployeeAmount>();

            foreach (var p in presences)
            {
                double numOfDays = (p.WorkEndTime.Date - p.WorkBeginTime.Date).TotalDays;
                if (numOfDays <= 1)
                {
                    int result = p.WorkBeginTime.Date.CompareTo(p.WorkEndTime.Date);
                    var existForEqual = model.Where(b => b.DayOfWork.Date == p.WorkBeginTime.Date).FirstOrDefault();
                    var existForGreater = model.Where(b => b.DayOfWork.Date == p.WorkEndTime.Date).FirstOrDefault();

                    switch (result)
                    {
                        case 0:
                            if (existForEqual != null)
                            {
                                foreach (var e in model.Where(b => b.DayOfWork.Date == p.WorkBeginTime.Date))
                                {
                                    e.AvgTime += (p.WorkEndTime.TimeOfDay - p.WorkBeginTime.TimeOfDay).TotalSeconds;
                                    e.divider = 1;

                                    if (!employees.Where(x => (x.EmployeeList.Contains(p.EmployeeId)) && (x.Day.Date == p.WorkBeginTime.Date)).Any())
                                    {
                                        foreach (var em in employees.Where(x => x.Day.Date == p.WorkBeginTime.Date))
                                        {
                                            em.EmployeeList.Add(p.EmployeeId);
                                            e.divider = em.EmployeeList.Count();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                AvgPresence presence = new AvgPresence();
                                presence.DayOfWork = p.WorkBeginTime;
                                presence.AvgTime += (p.WorkEndTime.TimeOfDay - p.WorkBeginTime.TimeOfDay).TotalSeconds;
                                presence.divider = 1;
                                employees.Add(new EmployeeAmount
                                {
                                    Day = presence.DayOfWork,
                                    EmployeeList = new List<int>() { p.EmployeeId },
                                });
                                if (!employees.Where(x => (x.EmployeeList.Contains(p.EmployeeId)) && (x.Day.Date == p.WorkBeginTime.Date)).Any())
                                {
                                    foreach (var e in employees.Where(x => x.Day.Date == p.WorkBeginTime.Date))
                                    {
                                        e.EmployeeList.Add(p.EmployeeId);
                                        presence.divider = e.EmployeeList.Count();
                                    }

                                }
                                model.Add(presence);

                            }
                            break;
                        case -1:

                            if (existForEqual != null)
                            {
                                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                                TimeSpan ts2 = new TimeSpan(p.WorkBeginTime.Hour, p.WorkBeginTime.Minute, p.WorkBeginTime.Second);
                                var totalTime = ts1 - ts2;
                                repository.RoundToSeconds(ref totalTime);
                                foreach (var e in model.Where(b => b.DayOfWork.Date == p.WorkBeginTime.Date))
                                {
                                    e.AvgTime += totalTime.TotalSeconds;

                                    if (!employees.Where(x => (x.EmployeeList.Contains(p.EmployeeId)) && (x.Day.Date == p.WorkBeginTime.Date)).Any())
                                    {
                                        foreach (var em in employees.Where(x => x.Day.Date == p.WorkBeginTime.Date))
                                        {
                                            em.EmployeeList.Add(p.EmployeeId);
                                            e.divider = em.EmployeeList.Count();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                AvgPresence presenceForEqual = new AvgPresence();
                                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                                TimeSpan ts2 = new TimeSpan(p.WorkBeginTime.Hour, p.WorkBeginTime.Minute, p.WorkBeginTime.Second);
                                var totalTime = ts1 - ts2;
                                repository.RoundToSeconds(ref totalTime);
                                DateTime dt = p.WorkBeginTime;
                                presenceForEqual.AvgTime = totalTime.TotalSeconds;
                                presenceForEqual.DayOfWork = dt;

                                employees.Add(new EmployeeAmount
                                {
                                    Day = presenceForEqual.DayOfWork,
                                    EmployeeList = new List<int>()
                                });

                                if (!employees.Where(x => (x.EmployeeList.Contains(p.EmployeeId) && x.Day.Date == dt.Date)).Any())
                                {
                                    foreach (var e in employees.Where(x => x.Day.Date == p.WorkBeginTime.Date))
                                    {
                                        e.EmployeeList.Add(p.EmployeeId);
                                        presenceForEqual.divider = e.EmployeeList.Count();
                                    }

                                }
                                model.Add(presenceForEqual);
                            }

                            if (existForGreater != null)
                            {
                                TimeSpan ts2 = new TimeSpan(p.WorkEndTime.Hour, p.WorkEndTime.Minute, p.WorkEndTime.Second);
                                repository.RoundToSeconds(ref ts2);
                                foreach (var e in model.Where(b => b.DayOfWork.Date == p.WorkEndTime.Date))
                                {
                                    e.AvgTime += ts2.TotalSeconds;

                                    if (!employees.Where(x => (x.EmployeeList.Contains(p.EmployeeId)) && (x.Day.Date == p.WorkEndTime.Date)).Any())
                                    {
                                        foreach (var em in employees.Where(x => x.Day.Date == p.WorkEndTime.Date))
                                        {
                                            em.EmployeeList.Add(p.EmployeeId);
                                            e.divider = em.EmployeeList.Count();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                AvgPresence presence = new AvgPresence();
                                TimeSpan ts2 = new TimeSpan(p.WorkEndTime.Hour, p.WorkEndTime.Minute, p.WorkEndTime.Second);
                                repository.RoundToSeconds(ref ts2);
                                presence.AvgTime += ts2.TotalSeconds;
                                presence.DayOfWork = p.WorkEndTime.Date + new TimeSpan(0, 0, 0);

                                if (!employees.Where(x => (x.EmployeeList.Contains(p.EmployeeId)) && (x.Day.Date == p.WorkEndTime.Date)).Any())
                                {
                                    employees.Add(new EmployeeAmount
                                    {
                                        Day = presence.DayOfWork,
                                        EmployeeList = new List<int>()
                                    });
                                    foreach (var e in employees.Where(x => x.Day.Date == p.WorkEndTime.Date))
                                    {
                                        e.EmployeeList.Add(p.EmployeeId);
                                        presence.divider = e.EmployeeList.Count();
                                    }
                                }
                                model.Add(presence);
                            }
                            break;
                    }
                }

            }
            foreach (var m in model)
            {
                int temp = (int)Math.Floor(m.AvgTime /= m.divider);
                m.AvgTime = temp;
            }
            foreach (var d in dates)
            {
                if (!model.Any(x => x.DayOfWork.Date == d.Date))
                {
                    model.Add(new AvgPresence
                    {
                        DayOfWork = d.Date
                    });
                }
            }

            model.Sort((x, y) => DateTime.Compare(x.DayOfWork, y.DayOfWork));
            return model;
        }
        public List<AvgBreaks> CreateAvgBreaks(List<Break> breaks, List<DateTime> dates)
        {
            List<AvgBreaks> model = new List<AvgBreaks>();
            List<EmployeeAmount> employees = new List<EmployeeAmount>();

            foreach (var b in breaks)
            {
                double numOfDays = (b.BreakEnd.Date - b.BreakStart.Date).TotalDays;
                if (numOfDays <= 1)
                {
                    int result = b.BreakStart.Date.CompareTo(b.BreakEnd.Date);
                    var existForEqual = model.Where(s => s.DayOfWork.Date == b.BreakStart.Date).FirstOrDefault();
                    var existForGreater = model.Where(s => s.DayOfWork.Date == b.BreakEnd.Date).FirstOrDefault();

                    switch (result)
                    {
                        case 0:
                            if (existForEqual != null)
                            {
                                foreach (var e in model.Where(s => s.DayOfWork.Date == b.BreakStart.Date))
                                {
                                    e.AvgBreakTime += (b.BreakEnd.TimeOfDay - b.BreakStart.TimeOfDay).TotalSeconds;
                                    e.divider = 1;

                                    if (!employees.Where(x => (x.EmployeeList.Contains(b.Presence.EmployeeId)) && (x.Day.Date == b.BreakStart.Date)).Any())
                                    {
                                        foreach (var em in employees.Where(x => x.Day.Date == b.BreakStart.Date))
                                        {
                                            em.EmployeeList.Add(b.Presence.EmployeeId);
                                            e.divider = em.EmployeeList.Count();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AvgBreaks breaksAvg = new AvgBreaks();
                                breaksAvg.DayOfWork = b.BreakStart;
                                breaksAvg.AvgBreakTime += (b.BreakEnd.TimeOfDay - b.BreakStart.TimeOfDay).TotalSeconds;
                                breaksAvg.divider = 1;
                                employees.Add(new EmployeeAmount
                                {
                                    Day = breaksAvg.DayOfWork,
                                    EmployeeList = new List<int>() { b.Presence.EmployeeId },
                                });
                                if (!employees.Where(x => (x.EmployeeList.Contains(b.Presence.EmployeeId)) && (x.Day.Date == b.BreakStart.Date)).Any())
                                {
                                    foreach (var e in employees.Where(x => x.Day.Date == b.BreakStart.Date))
                                    {
                                        e.EmployeeList.Add(b.Presence.EmployeeId);
                                        breaksAvg.divider = e.EmployeeList.Count();
                                    }

                                }
                                model.Add(breaksAvg);

                            }
                            break;
                        case -1:

                            if (existForEqual != null)
                            {
                                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                                TimeSpan ts2 = new TimeSpan(b.BreakStart.Hour, b.BreakStart.Minute, b.BreakStart.Second);
                                var totalTime = ts1 - ts2;
                                repository.RoundToSeconds(ref totalTime);
                                foreach (var e in model.Where(s => s.DayOfWork.Date == b.BreakStart.Date))
                                {
                                    e.AvgBreakTime += totalTime.TotalSeconds;

                                    if (!employees.Where(x => (x.EmployeeList.Contains(b.Presence.EmployeeId)) && (x.Day.Date == b.BreakStart.Date)).Any())
                                    {
                                        foreach (var em in employees.Where(x => x.Day.Date == b.BreakStart.Date))
                                        {
                                            em.EmployeeList.Add(b.Presence.EmployeeId);
                                            e.divider = em.EmployeeList.Count();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                AvgBreaks breakForEqual = new AvgBreaks();
                                TimeSpan ts1 = new TimeSpan(24, 0, 0);
                                TimeSpan ts2 = new TimeSpan(b.BreakStart.Hour, b.BreakStart.Minute, b.BreakStart.Second);
                                var totalTime = ts1 - ts2;
                                repository.RoundToSeconds(ref totalTime);
                                DateTime dt = b.BreakStart;
                                breakForEqual.AvgBreakTime = totalTime.TotalSeconds;
                                breakForEqual.DayOfWork = dt;

                                employees.Add(new EmployeeAmount
                                {
                                    Day = breakForEqual.DayOfWork,
                                    EmployeeList = new List<int>()
                                });

                                if (!employees.Where(x => (x.EmployeeList.Contains(b.Presence.EmployeeId) && x.Day.Date == dt.Date)).Any())
                                {
                                    foreach (var e in employees.Where(x => x.Day.Date == b.BreakStart.Date))
                                    {
                                        e.EmployeeList.Add(b.Presence.EmployeeId);
                                        breakForEqual.divider = e.EmployeeList.Count();
                                    }

                                }
                                model.Add(breakForEqual);
                            }

                            if (existForGreater != null)
                            {
                                TimeSpan ts2 = new TimeSpan(b.BreakEnd.Hour, b.BreakEnd.Minute, b.BreakEnd.Second);
                                repository.RoundToSeconds(ref ts2);
                                foreach (var e in model.Where(s => s.DayOfWork.Date == b.BreakEnd.Date))
                                {
                                    e.AvgBreakTime += ts2.TotalSeconds;

                                    if (!employees.Where(x => (x.EmployeeList.Contains(b.Presence.EmployeeId)) && (x.Day.Date == b.BreakEnd.Date)).Any())
                                    {
                                        foreach (var em in employees.Where(x => x.Day.Date == b.BreakEnd.Date))
                                        {
                                            em.EmployeeList.Add(b.Presence.EmployeeId);
                                            e.divider = em.EmployeeList.Count();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                AvgBreaks breaksAvg = new AvgBreaks();
                                TimeSpan ts2 = new TimeSpan(b.BreakEnd.Hour, b.BreakEnd.Minute, b.BreakEnd.Second);
                                repository.RoundToSeconds(ref ts2);
                                breaksAvg.AvgBreakTime += ts2.TotalSeconds;
                                breaksAvg.DayOfWork = b.BreakEnd.Date + new TimeSpan(0, 0, 0);

                                if (!employees.Where(x => (x.EmployeeList.Contains(b.Presence.EmployeeId)) && (x.Day.Date == b.BreakEnd.Date)).Any())
                                {
                                    employees.Add(new EmployeeAmount
                                    {
                                        Day = breaksAvg.DayOfWork,
                                        EmployeeList = new List<int>()
                                    });
                                    foreach (var e in employees.Where(x => x.Day.Date == b.BreakEnd.Date))
                                    {
                                        e.EmployeeList.Add(b.Presence.EmployeeId);
                                        breaksAvg.divider = e.EmployeeList.Count();
                                    }
                                }
                                model.Add(breaksAvg);
                            }
                            break;
                    }
                }

            }
            foreach (var m in model)
            {
                int temp = (int)Math.Floor(m.AvgBreakTime /= m.divider);
                m.AvgBreakTime = temp;
            }
            foreach (var d in dates)
            {
                if (!model.Any(x => x.DayOfWork.Date == d.Date))
                {
                    model.Add(new AvgBreaks
                    {
                        DayOfWork = d.Date
                    });
                }
            }

            model.Sort((x, y) => DateTime.Compare(x.DayOfWork, y.DayOfWork));
            return model;
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