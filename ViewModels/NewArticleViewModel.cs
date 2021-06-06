using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using PressAgency.Models;

namespace PressAgency.ViewModels {
  public class NewArticleViewModel {
    /* clang-format off */
    [Required]
    public ArticleType Type { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    [Display(Name = "Cover Image")]
    public IFormFile CoverImage { get; set; }
    /* clang-format on */
  }
}
