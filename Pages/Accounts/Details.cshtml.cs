using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WtLab12Banking.AuthValidation.Data;
using WtLab12Banking.AuthValidation.Models;

namespace WtLab12Banking.AuthValidation.Pages.Accounts;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Account? Account { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return Account is null ? NotFound() : Page();
    }
}
