using DarkClusterTechnologyEnterprise.Models.ViewModels;
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
                if (e.Position.Contains("Chief"))
                {
                    e.SuperiorId = chairman.EmployeeId;
                }
                else
                {
                    if (e.Position != "Chairman")
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
            var earnings = context.Earnings.OrderBy(i => i.Employee.UserId).ToList();

            for (int i = 0; i < earnings.Count(); i++)
            {
                string department = null;
                if (earnings[i].Employee.Department is null)
                {
                    department = "n/a";
                }
                else
                {
                    department = earnings[i].Employee.Department.DepartmentName;
                }
                earningsEmployees.Add(new EmployeesEarnings
                {
                    Login = loginByIDs[i].Login,
                    Firstname = earnings[i].Employee.Firstname,
                    Lastname = earnings[i].Employee.Surname,
                    DepartmentName = department,
                    Salary = earnings[i].Salary,
                    Premium = earnings[i].Premium,
                    Currency = earnings[i].Currency
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
                &&(b.WorkBeginTime.Date >= dateCriteria.Date))
                .OrderBy(b => b.WorkBeginTime)
                .ToList();

            return model;
        }
        public void CreatePresence(int eId)
        {
            //var t = DateTime.Now.ToUniversalTime();

            var p = context.Presences
                .Where(b => (b.WorkBegin == true) && (b.WorkEnd == false) && (b.EmployeeId == eId))
                .ToList();

            if (p.Count() == 0)
            {               
                DateTime workBeg = CheckIfDaylight(eId);
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
            var user = context.Employees.Where(e => e.EmployeeId == eId).FirstOrDefault();
            TimeZoneInfo userTZ = TimeZoneInfo.FindSystemTimeZoneById(user.Zone.TimeZoneID);
            DateTime time = DateTime.UtcNow;
            DateTime userTime = TimeZoneInfo.ConvertTime(time, userTZ);

            return userTime;
        }
        public void TakeBreak(string presenceId)
        {
            int eId = context.Presences
                .Where(p => p.PresenceId == presenceId)
                .Select(i => i.EmployeeId)
                .SingleOrDefault();

            if(presenceId != null)
            {
                var currentBreak = context.Breaks
                    .Where(i => (i.PresenceId == presenceId) && (i.Break_s == true))
                    .FirstOrDefault();

                if (currentBreak == null)
                {
                    DateTime breakBeg = CheckIfDaylight(eId);
                    Break newBreak = new Break();
                    newBreak.Break_s = true;
                    newBreak.BreakStart = breakBeg;
                    newBreak.PresenceId = presenceId;
                    context.Breaks.Add(newBreak);
                    context.SaveChanges();
                }
                else
                {
                    DateTime breakEnd = CheckIfDaylight(eId);
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
            DateTime time = CheckIfDaylight(eId);
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
                if(breakStop != null)
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
            foreach (var p in presencesId) {
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
            var breakActive = context.Breaks.Where(e =>( 
                e.Presence.EmployeeId == eId)&&
                (e.Break_s == true)&&
                (e.BreakStart.Date == d.Date)
                ).Any();

            return breakActive;
        }
        public double GetActiveBreakTime( int eId)
        {
            var breakStart = context.Breaks.Where(b => b.Break_s == true).FirstOrDefault();
            TimeSpan time = new TimeSpan();
            if(breakStart != null)
            {
                DateTime d = CheckIfDaylight(eId);
                time = (d - breakStart.BreakStart);
                RoundToSeconds(ref time);
            }

            return time.TotalSeconds;
        }

        public async Task<bool> CreateTasks(NewTask task, string username)
        {
            int eId = await GetEmployeeID(username);
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
        public List<TaskSchedule> GetAllTasks(int eId) => 
            context.TaskSchedules
            .Where(e => (e.EmployeeId == eId) 
            && e.TaskBegin.Date > DateTime.Now.Date)
            .ToList();
    }
}
