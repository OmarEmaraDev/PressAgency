using System;
using System.Collections.Generic;

namespace PressAgency.Models {
  public enum ArticleType { Sports, Cinema, Politics, Religion }
  public enum ArticleStatus { Accepted, Pending, Refused }

  public class Article {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public ArticleType Type { get; set; }
    public int NumberOfViews { get; set; } = 0;
    public string Image { get; set; }
    public ArticleStatus Status { get; set; } = ArticleStatus.Pending;

    public string AuthorID { get; set; }
    public ApplicationUser Author { get; set; }

    public List<Like> Likes { get; set; }

    public List<Dislike> Dislikes { get; set; }

    public List<Question> Questions { get; set; }

    public ICollection<ApplicationUser> Savers { get; set; }
  }
}
