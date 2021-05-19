using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PressAgency.Data {
  public class PressAgencyContext : IdentityDbContext {
    public PressAgencyContext(DbContextOptions<PressAgencyContext> options)
        : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }
  }
}
