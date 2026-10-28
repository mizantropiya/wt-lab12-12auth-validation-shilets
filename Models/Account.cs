using System.ComponentModel.DataAnnotations;

namespace WtLab12Banking.AuthValidation.Models;

public class Account
{
    public int Id { get; set; }

    [Required, StringLength(30)]
    public string AccountNumber { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string OwnerName { get; set; } = string.Empty;

    [Required, StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; } = "BYN";

    [Range(typeof(decimal), "0", "1000000000")]
    public decimal Balance { get; set; }

    [StringLength(50)]
    public string? Nickname { get; set; }

    [Required, StringLength(30)]
    public string AccountType { get; set; } = "Обычный счёт";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
}
