using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "The username field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The password field is required")]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
