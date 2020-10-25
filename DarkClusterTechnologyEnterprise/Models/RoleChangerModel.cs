using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models
{
    public class RoleChangerModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
    //public class User
}
