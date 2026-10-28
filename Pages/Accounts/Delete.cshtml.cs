using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WtLab12Banking.AuthValidation.Data;
using WtLab12Banking.AuthValidation.Models;

namespace WtLab12Banking.AuthValidation.Pages.Accounts;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public int Id { get; set; }

    public Account? Account { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (Account is null)
            return NotFound();

        Id = id;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var account = await _context.Accounts.FindAsync(Id);
        if (account is null)
            return NotFound();

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        TempData["Message"] = $"Счёт #{Id} удалён из PostgreSQL";
        return RedirectToPage("./Index");
    }
}
