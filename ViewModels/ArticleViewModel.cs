using PressAgency.Models;

namespace PressAgency.ViewModels {
  public class ArticleViewModel {
    public Article Article { get; set; }

    public bool IsLiked { get; set; }

    public bool IsDisliked { get; set; }

    public bool IsSaved { get; set; }

    public bool AllowActions { get; set; }
  }
}
