using Serilog;
using Duende.IdentityServer;

namespace IdentityServer
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            // uncomment if you want to add a UI
            //builder.Services.AddRazorPages();

            builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
            })  // in-memory, code config
                .AddTestUsers(TestUsers.Users)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryIdentityResources(Config.IdentityResources);

            builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });

            return builder.Build();
        }
        
        public static WebApplication ConfigurePipeline(this WebApplication app)
        { 
            app.UseSerilogRequestLogging();
        
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add a UI
            //app.UseStaticFiles();
            //app.UseRouting();
                
            app.UseIdentityServer();

            // uncomment if you want to add a UI
            //app.UseAuthorization();
            //app.MapRazorPages().RequireAuthorization();

            app.MapGet("/", () => "Identity Server!");

            return app;
        }
    }
}