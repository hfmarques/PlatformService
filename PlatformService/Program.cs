using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseInMemoryDatabase("InMemory"));
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"); });
    // using var scope = app.Services.CreateScope();
    // await using var db = scope.ServiceProvider.GetService<AppDbContext>();
    // await db!.Database.MigrateAsync();
}

app.PlatformApi();
app.Run();

namespace PlatformService
{
    public partial class Program
    {
    }
}