using Serilog;

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
            })
                .AddTestUsers(TestUsers.Users)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryIdentityResources(Config.IdentityResources);

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