using PressAgency.Models;
using PressAgency.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

public class LoginModalViewComponent : ViewComponent {
  private readonly SignInManager<ApplicationUser> _signInManager;

  public LoginModalViewComponent(SignInManager<ApplicationUser> signInManager) {
    _signInManager = signInManager;
  }

  public IViewComponentResult Invoke() {
    if (_signInManager.IsSignedIn(HttpContext.User)) {
      return Content(string.Empty);
    } else {
      LoginViewModel model = new LoginViewModel();
      return View(model);
    }
  }
}
