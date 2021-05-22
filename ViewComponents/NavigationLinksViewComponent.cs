using PressAgency.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

public class NavigationLinksViewComponent : ViewComponent {
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly UserManager<ApplicationUser> _userManager;

  public NavigationLinksViewComponent(
      SignInManager<ApplicationUser> signInManager,
      UserManager<ApplicationUser> userManager) {
    _signInManager = signInManager;
    _userManager = userManager;
  }

  public async Task<IViewComponentResult> InvokeAsync() {
    if (!_signInManager.IsSignedIn(HttpContext.User)) {
      return Content(string.Empty);
    }

    ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
    switch (user.Role) {
    case UserRole.Viewer:
      return View("Viewer");
    case UserRole.Editor:
      return View("Editor");
    case UserRole.Admin:
      return View("Admin");
    default:
      return Content(string.Empty);
    }
  }
}
