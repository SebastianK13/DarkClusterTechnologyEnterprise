﻿using DarkClusterTechnologyEnterprise.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private AppIdentityDbContext context;
        private UserManager<IdentityUser> userManager;
        public EFEmployeeRepository(AppIdentityDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public Employee CreateEmployee([Required]Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();

            return employee;
        }
        public void CreateEarnings(Earnings earnings)
        {
            context.Earnings.Add(earnings);
            context.SaveChanges();
        }
        public int GetLastEmployeeId() =>
            context.Employees.Select(x => x.EmployeeId).ToList().LastOrDefault();

        public void CreateLocations(Location location)
        {
            context.Locations.Add(location);
            context.SaveChanges();
        }
        public string GetLastLocation() =>
            context.Locations.Select(l => l.LocationId).ToList().LastOrDefault();

        public void CreateDepartments(Department department)
        {
            Department dept = context.Departments
                .Where(n => n.DepartmentName == department.DepartmentName)
                .FirstOrDefault();
            if (dept == null)
            {
                context.Departments.Add(department);
                context.SaveChanges();
            }
        }
        public int GetDepartmentId(string departmentName) =>
            context.Departments.Where(n => n.DepartmentName == departmentName)
            .Select(n => n.DepartmentId)
            .FirstOrDefault();

        public void SetChief(int deptId, int chiefId)
        {
            List<Department> departments = context.Departments.ToList();
            departments[deptId].ChiefId = chiefId;
            context.SaveChanges();
        }
        public void SetSuperior()
        {
            List<Employee> employees = context.Employees.ToList();
            //List<Department> depts = context.Departments.ToList();
            Employee chairman = employees[0];

            foreach (var e in employees)
            {
                if (e.Positions.PositionName.Contains("Chief"))
                {
                    e.SuperiorId = chairman.EmployeeId;
                }
                else
                {
                    if (e.Positions.PositionName != "Chairman")
                    {
                        e.SuperiorId = context.Departments
                            .Where(i => i.DepartmentId == e.DepartmentId)
                            .Select(c => c.ChiefId)
                            .FirstOrDefault();
                    }
                }
                context.SaveChanges();
            }
        }
        public List<Employee> GetEmployees(List<string> employeeIds)
        {
            List<Employee> employees = new List<Employee>();
            foreach (var e in employeeIds)
            {
                employees.Add(context.Employees
                    .Where(i => i.UserId == e)
                    .SingleOrDefault());
            }
            return employees;
        }
        public List<EmployeesEarnings> GetEarnings(List<LoginByID> loginByIDs)
        {
            List<EmployeesEarnings> earningsEmployees = new List<EmployeesEarnings>();
            var earnings = context.Employees.OrderBy(i => i.EmployeeId).ToList();

            for (int i = 0; i < earnings.Count(); i++)
            {
                string? department = null;
                if (earnings[i].Department is null)
                {
                    department = "n/a";
                }
                else
                {
                    department = earnings[i].Department?.DepartmentName;
                }
                earningsEmployees.Add(new EmployeesEarnings
                {
                    Login = loginByIDs[i].Login,
                    Firstname = earnings[i].Firstname,
                    Lastname = earnings[i].Surname,
                    DepartmentName = department,
                    Salary = earnings[i].Earnings?.Salary,
                    Premium = earnings[i].Earnings?.Premium,
                    Currency = earnings[i].Earnings?.Currency
                });
            }

            return earningsEmployees;
        }
        public Presence GetPresences(int eId)
        {
            var model = context.Presences
                .Where(b => (b.WorkBegin == true)
                && (b.WorkEnd == false)
                && (b.EmployeeId == eId))
                .FirstOrDefault();

            return model;
        }
        public List<Presence> GetPresenceAllUsers(DateTime dateCriteria)
        {
            var model = context.Presences
                .Where(b => (b.WorkBegin == true)
                && (b.WorkEnd == true)
                && (b.WorkBeginTime.Date >= dateCriteria.Date))
                .OrderBy(b => b.WorkBeginTime)
                .ToList();

            return model;
        }
        public void CreatePresence(int eId)
        {
            //var t = DateTime.Now.ToUniversalTime();

            var userTZ = GetUserTimeZone(eId);

            var p = context.Presences
                .Where(b => (b.WorkBegin == true) && (b.WorkEnd == false) && (b.EmployeeId == eId))
                .ToList();

            if (p.Count() == 0)
            {
                DateTime workBeg = CheckIfDaylight(userTZ);
                Presence presence = new Presence();
                DateTime time = DateTime.Now.ToUniversalTime();
                presence.EmployeeId = eId;
                presence.WorkBegin = true;
                presence.WorkBeginTime = workBeg;
                //presence.BreakTime = 0;
                context.Presences.Add(presence);
                context.SaveChanges();
            }

        }
        public DateTime CheckIfDaylight(int eId)
        {
            var userTZ = GetUserTimeZone(eId);
            return CheckIfDaylight(userTZ);
        }

        private DateTime CheckIfDaylight(TimeZoneInfo userTZ)
        {
            DateTime time = DateTime.UtcNow;
            DateTime userTime = TimeZoneInfo.ConvertTime(time, userTZ);

            return userTime;
        }
        private TimeZoneInfo GetUserTimeZone(int eId)
        {
            var user = context.Employees.Where(e => e.EmployeeId == eId).FirstOrDefault();
            return TimeZoneInfo.FindSystemTimeZoneById(user.Zone.TimeZoneID);
        }
        public void TakeBreak(string presenceId)
        {
            int eId = context.Presences
                .Where(p => p.PresenceId == presenceId)
                .Select(i => i.EmployeeId)
                .SingleOrDefault();

            var userTZ = GetUserTimeZone(eId);

            if (presenceId != null)
            {
                var currentBreak = context.Breaks
                    .Where(i => (i.PresenceId == presenceId) && (i.Break_s == true))
                    .FirstOrDefault();

                if (currentBreak == null)
                {
                    DateTime breakBeg = CheckIfDaylight(userTZ);
                    Break newBreak = new Break();
                    newBreak.Break_s = true;
                    newBreak.BreakStart = breakBeg;
                    newBreak.PresenceId = presenceId;
                    context.Breaks.Add(newBreak);
                    context.SaveChanges();
                }
                else
                {
                    DateTime breakEnd = CheckIfDaylight(userTZ);
                    TimeSpan t = breakEnd - currentBreak.BreakStart;
                    RoundToSeconds(ref t);
                    currentBreak.Break_s = false;
                    currentBreak.BreakTime += t.TotalSeconds;
                    currentBreak.BreakEnd = breakEnd;
                    context.Breaks.Attach(currentBreak);
                    context.SaveChanges();
                }
            }
        }
        public void WorkEnd(int eId)
        {
            var userTZ = GetUserTimeZone(eId);

            DateTime time = CheckIfDaylight(userTZ);
            var end = context.Presences
                .Where(b => (b.WorkBegin == true) &&
                (b.WorkEnd == false) &&
                (b.EmployeeId == eId))
                .FirstOrDefault();

            if (end != null)
            {
                var breakStop = context.Breaks.Where(b =>
                    (b.Break_s == true) &&
                    (b.Presence.EmployeeId == eId))
                    .FirstOrDefault();
                TimeSpan workTime = time - end.WorkBeginTime;
                RoundToMinutes(ref workTime);
                end.WorkTime = workTime.TotalSeconds;
                end.WorkEnd = true;
                end.WorkEndTime = time;
                if (breakStop != null)
                {
                    breakStop.BreakEnd = time;
                    breakStop.Break_s = false;
                    breakStop.BreakTime = (time - breakStop.BreakStart).TotalSeconds;
                    context.Breaks.Attach(breakStop);
                }
                context.Presences.Attach(end);
                context.SaveChanges();

            }
        }
        public void RoundToSeconds(ref TimeSpan t)
        {
            t = new TimeSpan(t.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond);
        }
        public void RoundToMinutes(ref TimeSpan t)
        {
            t = new TimeSpan(t.Ticks / TimeSpan.TicksPerMinute * TimeSpan.TicksPerMinute);
        }
        public List<Presence> GetAllPresences(int eId) =>
                context.Presences
                .Where(e => e.EmployeeId == eId)
                .ToList();

        public List<Break> GetAllBreaks(List<string> presencesId)
        {
            List<Break> breaks = new List<Break>();
            foreach (var p in presencesId)
            {
                var temp = context.Breaks.Where(b => b.PresenceId == p).ToList();
                if (temp != null)
                    breaks = breaks.Concat(temp).ToList();
                //breaks.Add(temp);
            }

            return breaks;
        }
        public List<Break> GetBreaksAllUsers(DateTime dateCriteria)
        {
            var model = context.Breaks
                .Where(b => (b.Break_s == false)
                && (b.BreakStart.Date >= dateCriteria.Date))
                .OrderBy(b => b.BreakStart)
                .ToList();

            return model;
        }
        public bool FindActiveBreak(int eId)
        {
            DateTime d = DateTime.Now.ToUniversalTime();
            var breakActive = context.Breaks.Where(e => (
                e.Presence.EmployeeId == eId) &&
                (e.Break_s == true) &&
                (e.BreakStart.Date == d.Date)
                ).Any();

            return breakActive;
        }
        public double GetActiveBreakTime(int eId)
        {
            double totalTime = 0;
            var presenceId = context.Presences
                .Where(i => i.EmployeeId == eId && !i.WorkEnd)
                .Select(i=>i.PresenceId)
                .FirstOrDefault();
            var userTZ = GetUserTimeZone(eId);
            var breaks = context.Breaks
                .Where(b => b.PresenceId == presenceId)
                .ToList();
            TimeSpan time = new TimeSpan();
            foreach (var b in breaks)
            {
                if (b.Break_s)
                {
                    DateTime d = CheckIfDaylight(userTZ);
                    time = (d - b.BreakStart);
                    RoundToSeconds(ref time);
                    totalTime += time.TotalSeconds;
                }
                else
                {
                    totalTime += b.BreakTime;
                }
            }

            return totalTime;
        }

        public async Task<bool> CreateTasks(NewTask task, int eId)
        {
            TaskSchedule newTask = new TaskSchedule(task, eId);
            context.TaskSchedules.Add(newTask);

            return await context.SaveChangesAsync() > 0;
        }
        public async Task<int> GetEmployeeID(string username) =>
            FindEmployeeId(await GetUserID(username));
        public int FindEmployeeId(string uId) =>
            context.Employees
            .Where(u => u.UserId == uId)
            .Select(e => e.EmployeeId)
            .FirstOrDefault();
        private async Task<string> GetUserID(string username)
        {
            IdentityUser user = await userManager.FindByNameAsync(username);
            return user.Id;
        }
        public List<TaskSchedule> GetAllTasks(int eId)
        {
            var date = ConvertToLocal(DateTime.UtcNow, eId);
            return context.TaskSchedules
                    .Where(e => (e.EmployeeId == eId)
                    && e.TaskBegin.Date >= date.Date)
                    .ToList();
        }

        public DateTime ConvertToLocal(DateTime date, int eId)
        {
            var userTZ = GetUserTimeZone(eId);
            return TimeZoneInfo.ConvertTime(date, userTZ);
        }

        public List<Subordinate> GetSubordinates(int eId) 
        {
           var subordinates = context.Employees
                .Where(s => s.SuperiorId == eId)
                .ToList();

            return new Subordinate(subordinates, eId).Subordinates;
        }
        public string GetNameSurname(int eId)
        {
           var fullName = context.Employees
                .Where(i => i.EmployeeId == eId)
                .Select(n => new { n.Firstname, n.Surname })
                .FirstOrDefault();

            return fullName.Firstname + ' ' + fullName.Surname;
        }
        public List<Employee> FindEmployeeByPhrase(string phrase) =>
            context.Employees
            .Where(x => (x.Firstname + ' ' + x.Surname)
            .Contains(phrase))
            .Take(5)
            .ToList();

        public List<TimeZonesModel> GetTimeZones() =>
            context.Zone.ToList();

        public List<Position> FindPositionByPhrase(string phrase) =>
            context.Positions
            .Where(x => (x.PositionName)
            .Contains(phrase))
            .Take(5)
            .ToList();

        public List<Department> FindDepartmentByPhrase(string phrase) =>
            context.Departments
            .Where(x => (x.DepartmentName)
            .Contains(phrase))
            .Take(5)
            .ToList();
    }
}
