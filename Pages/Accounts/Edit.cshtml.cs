using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WtLab12Banking.AuthValidation.Data;
using WtLab12Banking.AuthValidation.Models;

namespace WtLab12Banking.AuthValidation.Pages.Accounts;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public AccountInputModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (account is null)
            return NotFound();

        Id = id;
        Input = AccountInputModel.FromAccount(account);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var account = await _context.Accounts.FindAsync(Id);
        if (account is null)
            return NotFound();

        account.AccountNumber = Input.AccountNumber.Trim();
        account.OwnerName = Input.OwnerName.Trim();
        account.Currency = Input.Currency.Trim().ToUpperInvariant();
        account.Balance = Input.Balance;
        account.Nickname = string.IsNullOrWhiteSpace(Input.Nickname) ? null : Input.Nickname.Trim();
        account.AccountType = Input.AccountType;
        account.Description = Input.Description.Trim();

        await _context.SaveChangesAsync();
        TempData["Message"] = $"Счёт #{Id} изменён в PostgreSQL";
        return RedirectToPage("./Details", new { id = Id });
    }
}
