using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop.UI.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signInmanager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInmanager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInmanager.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}