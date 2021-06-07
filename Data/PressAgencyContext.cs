using PressAgency.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PressAgency.Data {
  public class PressAgencyContext : IdentityDbContext<ApplicationUser> {
    public PressAgencyContext(DbContextOptions<PressAgencyContext> options)
        : base(options) {}

    public DbSet<Article> Articles { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Dislike> Dislikes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Save> Saves { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }
  }
}
