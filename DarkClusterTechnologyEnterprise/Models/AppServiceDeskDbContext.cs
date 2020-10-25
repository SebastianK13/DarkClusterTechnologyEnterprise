using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DarkClusterTechnologyEnterprise.Models.ServiceDeskModels;

namespace DarkClusterTechnologyEnterprise.Models
{
    public class AppServiceDeskDbContext:DbContext
    {
        public AppServiceDeskDbContext(DbContextOptions<AppServiceDeskDbContext>
            options) : base(options){ }
        public DbSet<UserApplication> Applications { get; set; }
        public DbSet<ScheduledWork> Works { get; set; }
        public DbSet<ApplicationConversation> Conversations { get; set; }
    }
}
