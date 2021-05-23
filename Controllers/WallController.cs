using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace PressAgency.Controllers {
  public class WallController : Controller {
    public WallController() {}

    [AllowAnonymous]
    public IActionResult Index() { return View(); }

    [AllowAnonymous]
    public IActionResult LoginModal() { return Redirect("/#loginModal"); }

    [Authorize(Policy = "ViewerOnly")]
    public IActionResult Saved() { return View(); }
  }
}
