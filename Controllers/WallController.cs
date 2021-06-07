using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using PressAgency.Models;
using PressAgency.ViewModels;
using PressAgency.Data;

namespace PressAgency.Controllers {
  public class WallController : Controller {

    private readonly PressAgencyContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public WallController(PressAgencyContext context,
                          UserManager<ApplicationUser> userManager) {
      _context = context;
      _userManager = userManager;
    }

    [AllowAnonymous]
    public IActionResult Index() { return View(); }

    [AllowAnonymous]
    public IActionResult LoginModal() { return Redirect("/#loginModal"); }

    [Authorize(Policy = "ViewerOnly")]
    public IActionResult Saved() { return View(); }

    [AllowAnonymous]
    public async Task<IActionResult> Article(int? id) {
      if (id == null) {
        return NotFound();
      }

      Article article = await _context.Articles.Include(a => a.Author)
                            .FirstOrDefaultAsync(a => a.Id == id);
      if (article == null) {
        return NotFound();
      }

      article.NumberOfViews++;
      _context.SaveChanges();

      ApplicationUser user = await _userManager.GetUserAsync(User);

      Like like = await _context.Likes.FirstOrDefaultAsync(
          l => l.ArticleId == article.Id && l.UserId == user.Id);
      Dislike dislike = await _context.Dislikes.FirstOrDefaultAsync(
          l => l.ArticleId == article.Id && l.UserId == user.Id);
      Save save = await _context.Saves.FirstOrDefaultAsync(
          l => l.ArticleId == article.Id && l.UserId == user.Id);

      bool isLiked = like != null;
      bool isDisliked = dislike != null;
      bool isSaved = save != null;

      ArticleViewModel articleViewModel =
          new ArticleViewModel { Article = article, IsLiked = isLiked,
                                 IsDisliked = isDisliked, IsSaved = isSaved };

      return View(articleViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Like(int? id) {
      if (id == null) {
        return NotFound();
      }

      Article article =
          await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

      if (article == null) {
        return NotFound();
      }

      ApplicationUser user = await _userManager.GetUserAsync(User);

      Like like = new Like { Article = article, User = user };

      _context.Add(like);
      await _context.SaveChangesAsync();

      return RedirectToAction("Article", new { id = article.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Unlike(int? id) {
      if (id == null) {
        return NotFound();
      }

      Article article =
          await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

      if (article == null) {
        return NotFound();
      }

      ApplicationUser user = await _userManager.GetUserAsync(User);

      Like like = await _context.Likes.FirstOrDefaultAsync(
          l => l.ArticleId == article.Id && l.UserId == user.Id);

      if (like != null) {
        _context.Remove(like);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction("Article", new { id = article.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Dislike(int? id) {
      if (id == null) {
        return NotFound();
      }

      Article article =
          await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

      if (article == null) {
        return NotFound();
      }

      ApplicationUser user = await _userManager.GetUserAsync(User);

      Dislike dislike = new Dislike { Article = article, User = user };

      _context.Add(dislike);
      await _context.SaveChangesAsync();

      return RedirectToAction("Article", new { id = article.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Undislike(int? id) {
      if (id == null) {
        return NotFound();
      }

      Article article =
          await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

      if (article == null) {
        return NotFound();
      }

      ApplicationUser user = await _userManager.GetUserAsync(User);

      Dislike dislike = await _context.Dislikes.FirstOrDefaultAsync(
          l => l.ArticleId == article.Id && l.UserId == user.Id);

      if (dislike != null) {
        _context.Remove(dislike);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction("Article", new { id = article.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Save(int? id) {
      if (id == null) {
        return NotFound();
      }

      Article article =
          await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

      if (article == null) {
        return NotFound();
      }

      ApplicationUser user = await _userManager.GetUserAsync(User);

      Save save = new Save { Article = article, User = user };

      _context.Add(save);
      await _context.SaveChangesAsync();

      return RedirectToAction("Article", new { id = article.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Unsave(int? id) {
      if (id == null) {
        return NotFound();
      }

      Article article =
          await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

      if (article == null) {
        return NotFound();
      }

      ApplicationUser user = await _userManager.GetUserAsync(User);

      Save save = await _context.Saves.FirstOrDefaultAsync(
          l => l.ArticleId == article.Id && l.UserId == user.Id);

      if (save != null) {
        _context.Remove(save);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction("Article", new { id = article.Id });
    }
  }
}
