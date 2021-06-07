namespace PressAgency.Models {
  public class Save {
    public int Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int ArticleId { get; set; }
    public Article Article { get; set; }
  }
}
