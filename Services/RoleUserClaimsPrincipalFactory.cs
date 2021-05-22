using PressAgency.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace PressAgency.Services {
  public class RoleUserClaimsPrincipalFactory
      : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole> {
    public RoleUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor) {}

    public async override Task<ClaimsPrincipal>
    CreateAsync(ApplicationUser user) {
      ClaimsPrincipal principal = await base.CreateAsync(user);
      ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;

      var claims = new List<Claim>();
      switch (user.Role) {
      case UserRole.Viewer:
        claims.Add(new Claim(ClaimTypes.Role, "Viewer"));
        break;
      case UserRole.Editor:
        claims.Add(new Claim(ClaimTypes.Role, "Editor"));
        break;
      case UserRole.Admin:
        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        break;
      }

      identity.AddClaims(claims);
      return principal;
    }
  }
}
