using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WtLab12Banking.AuthValidation.Data;

namespace WtLab12Banking.AuthValidation.Pages;

public class LoginModel : PageModel
{
    private readonly AppDbContext _context;

    public LoginModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    [Required(ErrorMessage = "Введите логин")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Логин должен содержать от 3 до 50 символов")]
    public string Login { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(100, MinimumLength = 4,
        ErrorMessage = "Пароль должен содержать не менее 4 символов")]
    public string Password { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserLogin")))
            return RedirectToPage("/Profile");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == Login && x.Password == Password);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
            return Page();
        }

        HttpContext.Session.SetString("UserLogin", user.Login);
        TempData["Message"] = "Вход выполнен успешно.";
        return RedirectToPage("/Profile");
    }
}
