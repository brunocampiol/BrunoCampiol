using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrunoCampiol.Website.Pages.Identity
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public bool IsAuthenticated = false;
        public string Name;
        public string Provider;

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                IsAuthenticated = true;

                Name = User.Identity.Name;
                Provider = User.Identity.AuthenticationType;
            }
        }
    }
}