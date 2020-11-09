using DarkClusterTechnologyEnterprise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models
{
    public interface IEmployeeRepository
    {
        Task<int> GetEmployeeID(string username);
        int GetLastEmployeeId();
        Employee CreateEmployee(Employee employee);
        void CreateEarnings(Earnings earnings);
        void CreateLocations(Location location);
        string GetLastLocation();
        void CreateDepartments(Department department);
        int GetDepartmentId(string depts);
        void SetChief(int deptId, int chiefId);
        void SetSuperior();
        List<Employee> GetEmployees(List<string> employeeIds);
        List<EmployeesEarnings> GetEarnings(List<LoginByID> loginByIDs);
        Presence GetPresences(int eId);
        List<Presence> GetAllPresences(int eId);
        List<Presence> GetPresenceAllUsers(DateTime dateCriteria);
        List<Break> GetAllBreaks(List<string> presencesId);
        List<Break> GetBreaksAllUsers(DateTime dateCriteria);
        void CreatePresence(int eId);
        void TakeBreak(string presenceId);
        void WorkEnd(int eId);
        void RoundToSeconds(ref TimeSpan time);
        bool FindActiveBreak(int eId);
        double GetActiveBreakTime(int eId);
        DateTime CheckIfDaylight(int eId);
        Task<bool> CreateTasks(NewTask task, string username);
        List<TaskSchedule> GetAllTasks(int eId);
        DateTime ConvertToLocal(DateTime date, int eId);
        //Profile Controller
    }
}
