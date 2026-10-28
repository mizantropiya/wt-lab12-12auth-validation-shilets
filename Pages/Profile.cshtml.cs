using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WtLab12Banking.AuthValidation.Pages;

public class ProfileModel : PageModel
{
    public string? UserLogin { get; private set; }

    public IActionResult OnGet()
    {
        UserLogin = HttpContext.Session.GetString("UserLogin");
        if (string.IsNullOrWhiteSpace(UserLogin))
            return RedirectToPage("/Login");

        return Page();
    }

    public IActionResult OnPost()
    {
        HttpContext.Session.Clear();
        TempData["Message"] = "Вы вышли из системы.";
        return RedirectToPage("/Login");
    }
}
