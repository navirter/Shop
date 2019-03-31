using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private IHostingEnvironment _env;

        public CustomerInformationModel(IHostingEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            //get cart
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            //if cart exists go to payment
            if (information == null)
            {
                if (_env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        FirstName = "a",
                        LastName = "a",
                        Email = "a@a.com",
                        PhoneNumber = "11",
                        Address1 = "a",
                        Address2 = "a",
                        City = "a",
                        PostCode = "a"
                    };
                }
                return Page();
            }
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