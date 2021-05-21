using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PressAgency.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace PressAgency.Controllers {
  public class AccountController : Controller {

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager,
                             SignInManager<IdentityUser> signInManager) {
      _signInManager = signInManager;
      _userManager = userManager;
    }

    [HttpPost]
    public async Task<JsonResult> Login(LoginViewModel user) {
      FormValidationViewModel validation = new FormValidationViewModel();
      if (!ModelState.IsValid) {
        validation.Valid = false;
        validation.Error = "Invalid login attempt.";
        return Json(validation);
      }

      SignInResult result = await _signInManager.PasswordSignInAsync(
          user.UserName, user.Password, user.RememberMe, false);

      if (!result.Succeeded) {
        validation.Valid = false;
        validation.Error = "Invalid username or password.";
        return Json(validation);
      }

      return Json(validation);
    }

    [HttpPost]
    public async Task<JsonResult> Register(RegisterViewModel user) {
      FormValidationViewModel validation = new FormValidationViewModel();
      if (!ModelState.IsValid) {
        validation.Valid = false;
        validation.Error = "Invalid registration attempt.";
        return Json(validation);
      }

      IdentityUser identityUser =
          new IdentityUser { UserName = user.UserName, Email = user.Email };
      IdentityResult identityResult =
          await _userManager.CreateAsync(identityUser, user.Password);

      if (!identityResult.Succeeded) {
        validation.Valid = false;
        validation.Error = "Username already exists.";
        return Json(validation);
      }

      SignInResult signInResult = await _signInManager.PasswordSignInAsync(
          user.UserName, user.Password, false, false);

      return Json(validation);
    }

    [HttpPost]
    public async Task<IActionResult> Logout() {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Wall");
    }
  }
}
