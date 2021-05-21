using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

public class LoginModalViewComponent : ViewComponent {
  private readonly SignInManager<IdentityUser> _signInManager;

  public LoginModalViewComponent(SignInManager<IdentityUser> signInManager) {
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
