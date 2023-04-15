using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace Bookstore.FrontEnd.Configuration
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        private readonly IConfiguration _configuration;  
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigation,
            IConfiguration configuration)
            : base(provider, navigation)
        {
            _configuration = configuration;
            ConfigureHandler(
                authorizedUrls: new[] { _configuration["Auth0:Audience"] },
                scopes: new[] { _configuration["Auth0:Audience"] });

        }
    }
}
