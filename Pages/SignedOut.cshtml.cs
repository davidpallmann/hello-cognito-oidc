using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace hello_cognito_oidc.Pages
{
    public class SignedOutModel : PageModel
    {
        private readonly ILogger<SignedOutModel> _logger;

        public SignedOutModel(ILogger<SignedOutModel> logger)
        {
            _logger = logger;
        }

    }
}
