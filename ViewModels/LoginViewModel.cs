using System.ComponentModel.DataAnnotations;

namespace PressAgency.ViewModels {
  public class LoginViewModel {
    /* clang-format off */
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
    /* clang-format on */
  }
}
