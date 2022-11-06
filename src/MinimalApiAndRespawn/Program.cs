using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence;
using MinimalApiAndRespawn.Features.Customers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext")));

builder.Services.AddCustomerFeature();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(config => config.ConfigureDefaults());

if(app.Environment.IsDevelopment())
{
    using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDbContext.Database.EnsureCreated();
}

app.Run();