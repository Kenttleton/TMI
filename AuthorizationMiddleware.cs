using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMI
{
    public class AuthorizationMiddleware
    {
        public AuthorizationMiddleware() {
            
        }

        public void AddAuthenticationService(IServiceCollection services)
        {
            services.AddAuthentication().AddTwitch(options =>
            {
                options.ClientId = "";
                options.ClientSecret = "";
                options.ForceVerify = false;
            });

            services.AddAuthorization();
        }
    }
}
