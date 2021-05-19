using System;
using System.Linq;
using PressAgency.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PressAgency {
public class Startup {
  public Startup(IConfiguration configuration) {
    Configuration = configuration;
  }

  public IConfiguration Configuration { get; }

  public void ConfigureServices(IServiceCollection services) {
    services.AddControllersWithViews();

    string connectionString = Configuration.GetConnectionString("postgresSQL");
    services.AddDbContext<PressAgencyContext>(
        options => options.UseNpgsql(connectionString));

    services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<PressAgencyContext>();
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
    if (env.IsDevelopment()) {
      app.UseDeveloperExceptionPage();
    } else {
      app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints => {
      endpoints.MapControllerRoute(
          name: "default", pattern: "{controller=Wall}/{action=Index}/{id?}");
    });
  }
}
}
