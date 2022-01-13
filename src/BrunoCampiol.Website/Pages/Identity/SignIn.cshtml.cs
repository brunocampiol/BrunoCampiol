using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrunoCampiol.UI.Web.Pages.Identity
{
    public class SignInModel : PageModel
    {
        public IActionResult OnGet(string provider)
        {
            // TODO: do not use hard coded string
            string returnUrl = String.Empty;
            
            if (HttpContext.Request.Host.Host.Contains("localhost")) returnUrl = "http://localhost:2000/Identity";
            else returnUrl = "https://brunocampiol.com/Identity";

            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);
        }
    }
}