using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WtLab12Banking.AuthValidation.Data;
using WtLab12Banking.AuthValidation.Models;

namespace WtLab12Banking.AuthValidation.Pages.Accounts;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public IReadOnlyList<Account> Accounts { get; private set; } = Array.Empty<Account>();

    public async Task OnGetAsync()
    {
        var query = _context.Accounts.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            var term = Search.Trim().ToLower();
            query = query.Where(account =>
                account.AccountNumber.ToLower().Contains(term) ||
                account.OwnerName.ToLower().Contains(term) ||
                (account.Nickname != null && account.Nickname.ToLower().Contains(term)));
        }

        Accounts = await query.OrderBy(account => account.Id).ToListAsync();
    }
}
