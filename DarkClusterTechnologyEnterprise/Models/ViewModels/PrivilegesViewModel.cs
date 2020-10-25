using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class PrivilegesViewModel
    {
        public IEnumerable<EmployeeDetails> Employee { get; set; }
        public PageInfo PagesInfo { get; set; }
    }
    public class EmployeeDetails
    {
        public string Login { get; set; }
        public string RoleName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DepartmentName { get; set; }
    }
}
