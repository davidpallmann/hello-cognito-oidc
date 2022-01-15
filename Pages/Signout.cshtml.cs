using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace hello_cognito_oidc.Pages
{
    public class SignOutModel : PageModel
    {
        private readonly ILogger<SignOutModel> _logger;

        public SignOutModel(ILogger<SignOutModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var callbackUrl = Url.Page("/SignedOut", pageHandler: null, values: null, protocol: Request.Scheme);
            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme
            );
        }
    }
}
