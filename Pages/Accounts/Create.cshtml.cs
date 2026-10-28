using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WtLab12Banking.AuthValidation.Data;
using WtLab12Banking.AuthValidation.Models;

namespace WtLab12Banking.AuthValidation.Pages.Accounts;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public AccountInputModel Input { get; set; } = new() { Currency = "BYN", AccountType = "Обычный счёт" };

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var account = new Account
        {
            AccountNumber = Input.AccountNumber.Trim(),
            OwnerName = Input.OwnerName.Trim(),
            Currency = Input.Currency.Trim().ToUpperInvariant(),
            Balance = Input.Balance,
            Nickname = string.IsNullOrWhiteSpace(Input.Nickname) ? null : Input.Nickname.Trim(),
            AccountType = Input.AccountType,
            Description = Input.Description.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        TempData["Message"] = $"Счёт #{account.Id} создан и сохранён в PostgreSQL";
        return RedirectToPage("./Details", new { id = account.Id });
    }
}
