using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PressAgency.Models {
  public enum UserRole { Viewer, Editor, Admin }

  public class ApplicationUser : IdentityUser {
    /* clang-format off */
    [PersonalData]
    public string FirstName { get; set; }

    [PersonalData]
    public string LastName { get; set; }

    public string Image { get; set; }

    public UserRole Role { get; set; }

    public List<Article> Articles { get; set; }

    public List<Like> Likes { get; set; }

    public List<Dislike> Dislikes { get; set; }

    public List<Question> Questions { get; set; }
    /* clang-format on */
  }
}
