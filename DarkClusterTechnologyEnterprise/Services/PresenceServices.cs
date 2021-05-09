using DarkClusterTechnologyEnterprise.Models;
using DarkClusterTechnologyEnterprise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Services
{
    public class PresenceServices
    {
        private readonly IEmployeeRepository _repository;
        public PresenceServices(IEmployeeRepository repository) =>
            _repository = repository;
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
        public List<GraphViewModel> WeeklyStats(List<Presence> presences)
        {
            List<GraphViewModel> model = new List<GraphViewModel>();

            foreach (var p in presences)
            {
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
                                _repository.RoundToSeconds(ref totalTime);
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
                                _repository.RoundToSeconds(ref totalTime);
                                DateTime dt = p.WorkBeginTime;
                                presenceForEqual.WorkTime = totalTime.TotalSeconds;
                                presenceForEqual.WorkBeginTime = dt;
                                model.Add(presenceForEqual);
                            }

                            if (existForGreater != null)
                            {
                                TimeSpan ts2 = new TimeSpan(p.WorkEndTime.Hour, p.WorkEndTime.Minute, p.WorkEndTime.Second);
                                _repository.RoundToSeconds(ref ts2);
                                foreach (var e in model.Where(b => b.WorkBeginTime.Date == p.WorkEndTime.Date))
                                {
                                    e.WorkTime += ts2.TotalSeconds;
                                }
                            }
                            else
                            {
                                TimeSpan ts2 = new TimeSpan(p.WorkEndTime.Hour, p.WorkEndTime.Minute, p.WorkEndTime.Second);
                                _repository.RoundToSeconds(ref ts2);
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
                                _repository.RoundToSeconds(ref totalTime);
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
                                _repository.RoundToSeconds(ref totalTime);
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
                                _repository.RoundToSeconds(ref ts2);
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
                                _repository.RoundToSeconds(ref ts2);
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
                                _repository.RoundToSeconds(ref totalTime);
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
                                _repository.RoundToSeconds(ref totalTime);
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
                                _repository.RoundToSeconds(ref ts2);
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
                                _repository.RoundToSeconds(ref ts2);
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
        public void InsertBreaksToModel(ref PresenceViewModel pVM)
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
                                foreach (var w in pVM.DataModel.Where(y => y.WorkBeginTime.Date == p.BreakStart.Date))
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
                                _repository.RoundToSeconds(ref totalTime);
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
                                    _repository.RoundToSeconds(ref totalTime);
                                    DateTime dt = p.BreakStart;
                                    w.BreakTime = totalTime.TotalSeconds;
                                    w.BreakStart = dt;
                                }
                            }

                            if (existForGreater != null)
                            {
                                TimeSpan ts2 = new TimeSpan(p.BreakEnd.Hour, p.BreakEnd.Minute, p.BreakEnd.Second);
                                _repository.RoundToSeconds(ref ts2);
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
                                    _repository.RoundToSeconds(ref ts2);
                                    w.BreakTime += ts2.TotalSeconds;
                                    w.BreakStart = p.BreakEnd.Date + new TimeSpan(0, 0, 0);
                                }

                            }
                            break;
                    }
                }

            }
        }
        public async Task<PresenceViewModel> GetPresenceModel(string name, int opt = 0, string category = "work", string clientTimeZone = "")
        {            
            int eId = await _repository.GetEmployeeID(name);
            Presence model = _repository.GetPresences(eId);
            PresenceViewModel presenceModel = new PresenceViewModel();
            var allPresences = _repository.GetAllPresences(eId);
            var allBreaks = _repository.GetAllBreaks(allPresences.Select(x => x.PresenceId).ToList());
            DateTime dateCriteria = DateTime.Now.ToUniversalTime().AddDays(-7);
            createMonth(ref month);

            switch (opt)
            {
                case 1:
                    dateCriteria = DateTime.Now.ToUniversalTime().AddDays(-31);
                    presenceModel.Presences = allPresences.Where(b => b.WorkBeginTime.Date >= dateCriteria.Date).OrderBy(b => b.WorkBeginTime).ToList();
                    presenceModel.DataModel = WeeklyStats(presenceModel.Presences);
                    presenceModel.Breaks = allBreaks.Where(b => b.BreakStart.Date >= dateCriteria.Date).OrderBy(b => b.BreakStart).ToList();
                    fillRange(ref presenceModel, month);
                    //todo: fillRange for Avgs 
                    presenceModel.AvgPresences = AvgPresences(_repository.GetPresenceAllUsers(dateCriteria), month);
                    InsertBreaksToModel(ref presenceModel);
                    presenceModel.AvgBreaks = CreateAvgBreaks(_repository.GetBreaksAllUsers(dateCriteria), month);
                    presenceModel.AvgPresences = AvgPresences(_repository.GetPresenceAllUsers(dateCriteria), month);
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
                    presenceModel.AvgBreaks = CreateAvgBreaks(_repository.GetBreaksAllUsers(dateCriteria), datesRange);
                    presenceModel.AvgPresences = AvgPresences(_repository.GetPresenceAllUsers(dateCriteria), datesRange);
                    fillRangeWTAvg(ref presenceModel, datesRange);
                    fillRangeBreakAvg(ref presenceModel, datesRange);

                    break;
                default:
                    presenceModel.Presences = allPresences.Where(b => b.WorkBeginTime.Date >= dateCriteria.Date).OrderBy(b => b.WorkBeginTime).ToList();
                    presenceModel.DataModel = WeeklyStats(presenceModel.Presences);
                    presenceModel.Breaks = allBreaks.Where(b => b.BreakStart.Date >= dateCriteria.Date).OrderBy(b => b.BreakStart).ToList();
                    fillRange(ref presenceModel, week);
                    InsertBreaksToModel(ref presenceModel);
                    presenceModel.AvgBreaks = CreateAvgBreaks(_repository.GetBreaksAllUsers(dateCriteria), week);
                    presenceModel.AvgPresences = AvgPresences(_repository.GetPresenceAllUsers(dateCriteria), week);
                    fillRangeWTAvg(ref presenceModel, week);
                    fillRangeBreakAvg(ref presenceModel, week);
                    break;
            }
            if (model != null)
            {
                TimeSpan current = _repository.CheckIfDaylight(eId) - model.WorkBeginTime;
                _repository.RoundToSeconds(ref current);
                presenceModel.BreakActive = _repository.FindActiveBreak(eId);
                double seconds = current.TotalSeconds;
                presenceModel.Seconds = seconds;
                presenceModel.BreakLength = _repository.GetActiveBreakTime(eId);
            }
            else
            {
                presenceModel.Seconds = -1;
            }
            return presenceModel;
        }
    }
}
