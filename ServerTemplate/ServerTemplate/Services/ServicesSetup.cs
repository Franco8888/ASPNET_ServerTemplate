using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Sets up all the services. Called in the startup
namespace ServerTemplate.Services
{
    public class ServicesSetup
    {
        public static void SetupServices(IServiceCollection services)
        {
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<IDatabaseService, DatabaseService>();
        }
    }
}
