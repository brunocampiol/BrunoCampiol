using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrunoCampiol.Website.Pages.Identity
{
    public class IndexModel : PageModel
    {

        public IActionResult OnGet(string provider)
        {
            if (provider == null) return Page();

            string returnUrl = "http://localhost:2000";

            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);
        }

        //public void OnGet(string provider)
        //{
        //    string returnUrl = "/Identity/Profile";
        //    string myprovider = "";

        //    Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);
        //}
    }
}