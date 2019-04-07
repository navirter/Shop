using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private SignInManager<IdentityUser> _signManager;

        public LoginModel(SignInManager<IdentityUser> signManager)
        {
            _signManager = signManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _signManager.PasswordSignInAsync(Input.Username, Input.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToPage("/Admin/Index");
            }
            else
            {
                return Page();
            }
        }
    }
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}