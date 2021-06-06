using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using PressAgency.ViewModels;
using PressAgency.Models;
using PressAgency.Data;
using PressAgency.Utils;

namespace PressAgency.Controllers {
  public class FactoryController : Controller {

    private readonly PressAgencyContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UserManager<ApplicationUser> _userManager;

    public FactoryController(PressAgencyContext context,
                             IWebHostEnvironment webHostEnvironment,
                             UserManager<ApplicationUser> userManager) {
      _context = context;
      _userManager = userManager;
      _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult NewArticle() { return View(); }

    [HttpPost]
    public async Task<IActionResult>
    NewArticle(NewArticleViewModel newArticle) {
      if (!ModelState.IsValid) {
        return View(newArticle);
      }

      if (!newArticle.CoverImage.IsPNGImage()) {
        ModelState.AddModelError(nameof(newArticle.CoverImage),
                                 "Invalid PNG image.");
        return View(newArticle);
      }

      string wwwrootDirectory = _webHostEnvironment.WebRootPath;
      string imagesDirectory = Path.Combine(wwwrootDirectory, "images");
      string imageName = Path.GetRandomFileName() + ".png";
      string imagePath = Path.Combine(imagesDirectory, imageName);
      using (FileStream fileStream =
                 new FileStream(imagePath, FileMode.Create)) {
        newArticle.CoverImage.CopyTo(fileStream);
      }

      ApplicationUser user = await _userManager.GetUserAsync(User);

      Article article =
          new Article { Type = newArticle.Type, Title = newArticle.Title,
                        Body = newArticle.Body, Image = imageName,
                        Author = user };

      _context.Add(article);
      await _context.SaveChangesAsync();

      return RedirectToAction("MyArticles");
    }

    public async Task<IActionResult> MyArticles() {
      ApplicationUser user = await _userManager.GetUserAsync(User);
      return View(await _context.Articles.Where(a => a.Author == user)
                      .Include(a => a.Author)
                      .ToListAsync());
    }
  }
}
