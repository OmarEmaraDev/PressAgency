using System;
using Microsoft.AspNetCore.Identity;

namespace PressAgency.Models {

  public enum UserRole { Viewer, Editor, Admin }

  public class ApplicationUser : IdentityUser {
    /* clang-format off */
    [PersonalData]
    public string FirstName { get; set; }

    [PersonalData]
    public string LastName { get; set; }

    public UserRole Role { get; set; }
    /* clang-format on */
  }
}
