using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

public class RegisterModalViewComponent : ViewComponent {
  private readonly SignInManager<IdentityUser> _signInManager;

  public RegisterModalViewComponent(SignInManager<IdentityUser> signInManager) {
    _signInManager = signInManager;
  }

  public IViewComponentResult Invoke() {
    if (_signInManager.IsSignedIn(HttpContext.User)) {
      return Content(string.Empty);
    } else {
      return View();
    }
  }
}
