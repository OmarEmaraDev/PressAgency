using System;
using Microsoft.AspNetCore.Identity;

namespace PressAgency.Models {
  public class ApplicationUser : IdentityUser {
    /* clang-format off */
    [PersonalData]
    public string FirstName { get; set; }

    [PersonalData]
    public string LastName { get; set; }
    /* clang-format on */
  }
}
