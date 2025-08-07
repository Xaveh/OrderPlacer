using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDiscovery();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedWindow", o =>
    {
        o.Window = TimeSpan.FromSeconds(10);
        o.PermitLimit = 10;
    });

    options.RejectionStatusCode = 429;
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

var app = builder.Build();

app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.MapReverseProxy();

app.Run();