using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class EarningsViewModel
    {
        public PageInfo PagesInfo { get; set; }
        public IEnumerable<EmployeesEarnings> employeesEarnings{get;set;}
    }
    public class EmployeesEarnings 
    {
        public string Login { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? DepartmentName { get; set; }
        public string? Currency { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Premium { get; set; }
    }

}
