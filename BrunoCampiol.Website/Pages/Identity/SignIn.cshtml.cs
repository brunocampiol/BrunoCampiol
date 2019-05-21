using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrunoCampiol.Website.Pages.Identity
{
    public class SignInModel : PageModel
    {
        public IActionResult OnGet(string provider)
        {
            string returnUrl = "http://localhost:2000";

            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);
        }
    }
}