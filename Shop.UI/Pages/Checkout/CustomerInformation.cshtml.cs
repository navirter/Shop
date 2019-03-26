using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {

        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            //get cart
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            //if cart exists go to payment
            if (information == null)
                return Page();
            else
                return RedirectToPage("/Checkout/Payment");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //post cart
            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);
            return RedirectToPage("/Checkout/Payment");
        }
    }
}