using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.UsersAdmin
{
    public class CreateUser
    {
        private UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class Request
        {
            public string UserName { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var managerUser = new IdentityUser()
            {
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(managerUser, "password");//.GetAwaiter().GetResult();
            var managerClaim = new Claim("Role", "Manager");
            var result2 = await _userManager.AddClaimAsync(managerUser, managerClaim);//.GetAwaiter().GetResult();
            return true;
        }
    }
}
