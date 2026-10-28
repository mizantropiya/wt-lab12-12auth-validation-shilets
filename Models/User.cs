using System.ComponentModel.DataAnnotations;

namespace WtLab12Banking.AuthValidation.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Login { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
}
