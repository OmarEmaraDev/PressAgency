using System;
using System.Linq;
using PressAgency.Data;
using PressAgency.Models;
using PressAgency.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
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

    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<PressAgencyContext>();

    services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                       RoleUserClaimsPrincipalFactory>();

    services.Configure<IdentityOptions>(options => {
      options.Password.RequireDigit = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireUppercase = false;
      options.Password.RequiredLength = 6;
      options.Password.RequiredUniqueChars = 1;
    });

    services.ConfigureApplicationCookie(options => {
      options.LoginPath = "/Wall/loginModal/";
      options.AccessDeniedPath = "/";
    });

    services.AddAuthorization(options => {
      options.FallbackPolicy =
          new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

      options.AddPolicy(
          "AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
      options.AddPolicy("EditorOnly", policy => policy.RequireClaim(
                                          ClaimTypes.Role, "Editor"));
      options.AddPolicy("ViewerOnly", policy => policy.RequireClaim(
                                          ClaimTypes.Role, "Viewer"));
    });
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
