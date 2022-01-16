using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Real_Estate.Areas.Identity.Data;
using Real_Estate.Data;

[assembly: HostingStartup(typeof(Real_Estate.Areas.Identity.IdentityHostingStartup))]
namespace Real_Estate.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Real_EstateContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Real_EstateContext")));

                //services.AddDefaultIdentity<Real_EstateUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 //   .AddEntityFrameworkStores<Real_EstateContext>();
            });
        }
    }
}