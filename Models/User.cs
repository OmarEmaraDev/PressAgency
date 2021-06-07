using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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

    [InverseProperty("Author")]
    public List<Article> AuthoredArticles { get; set; }

    public List<Like> Likes { get; set; }

    public List<Dislike> Dislikes { get; set; }

    public List<Question> Questions { get; set; }

    public List<Save> Saves { get; set; }
    /* clang-format on */
  }
}
