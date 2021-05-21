using PressAgency.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

public class LoginStatusViewComponent : ViewComponent {
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly UserManager<ApplicationUser> _userManager;

  public LoginStatusViewComponent(SignInManager<ApplicationUser> signInManager,
                                  UserManager<ApplicationUser> userManager) {
    _signInManager = signInManager;
    _userManager = userManager;
  }

  public async Task<IViewComponentResult> InvokeAsync() {
    if (_signInManager.IsSignedIn(HttpContext.User)) {
      ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
      return View("LoggedIn", user);
    } else {
      return View();
    }
  }
}
