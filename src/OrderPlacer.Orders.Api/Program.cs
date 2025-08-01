using FastEndpoints;
using OrderPlacer.Orders.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseCosmos(builder.Configuration.GetConnectionString("CosmosDB")!, "OrdersDatabase"));

var app = builder.Build();

app.UseFastEndpoints();

app.Run();