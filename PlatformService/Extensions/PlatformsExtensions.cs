using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Extensions;

public static class PlatformsExtensions
{
    public static void PlatformApi(this WebApplication webApplication)
    {
        webApplication.MapGet("/Platform/", (IPlatformRepository repository) =>
        {
            var platforms = repository.GetAll();
            return Results.Ok(platforms);
        }).Produces<Platform>();
        
        webApplication.MapGet("/Platform/id/{id}", (int id, IPlatformRepository repository) =>
        {
            var platform = repository.GetById(id);
            return platform is null ? 
                Results.NotFound(platform) : 
                Results.Ok(platform);
        }).Produces<Platform>()
            .Produces(StatusCodes.Status404NotFound);
        
        webApplication.MapPost("/Platform/", (Platform platform, IPlatformRepository repository) =>
        {
            repository.CreatePlatform(platform);
            repository.SaveChanges();
            return Results.Created($"/Platform/id/{platform.Id}", platform);
        }).Produces<Platform>(StatusCodes.Status201Created);
    }   
}