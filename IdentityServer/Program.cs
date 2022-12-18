using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    options.EmitStaticAudienceClaim = true;
})
    .AddTestUsers(TestUsers.Users)
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources);

var app = builder.Build();

app.UseIdentityServer();

app.MapGet("/", () => "Identity Server!");

app.Run();
