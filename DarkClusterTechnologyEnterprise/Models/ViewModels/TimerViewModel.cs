using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Models.ViewModels
{
    public class TimerViewModel
    {
        public bool stop { get; set; } = false;
        public bool begin { get; set; } = false;
        public bool break_w { get; set; } = false;
    }
}
