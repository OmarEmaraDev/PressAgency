using PressAgency.Models;
using PressAgency.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

public class RegisterModalViewComponent : ViewComponent {
  private readonly SignInManager<ApplicationUser> _signInManager;

  public RegisterModalViewComponent(
      SignInManager<ApplicationUser> signInManager) {
    _signInManager = signInManager;
  }

  public IViewComponentResult Invoke() {
    if (_signInManager.IsSignedIn(HttpContext.User)) {
      return Content(string.Empty);
    } else {
      RegisterViewModel model = new RegisterViewModel();
      return View(model);
    }
  }
}
