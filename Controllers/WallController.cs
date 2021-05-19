using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PressAgency.Controllers {
  public class WallController : Controller {
    public WallController() {}

    public IActionResult Index() { return View(); }
  }
}
