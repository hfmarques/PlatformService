using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dto;
using PlatformService.Models;

namespace PlatformService.Extensions;

public static class PlatformsExtensions
{
    public static void PlatformApi(this WebApplication webApplication)
    {
        webApplication.MapGet("/Platform/", (IPlatformRepository repository, IMapper mapper) =>
        {
            var platforms = repository.GetAll();
            var platformsDto = mapper.Map<List<PlatformDto>>(platforms);
            return Results.Ok(platformsDto);
        }).Produces<PlatformDto>();
        
        webApplication.MapGet("/Platform/id/{id}", (int id, IPlatformRepository repository, IMapper mapper) =>
        {
            var platform = repository.GetById(id);
            if(platform is null)
                return Results.NotFound();

            var platformDto = mapper.Map<PlatformDto>(platform);
            
            return Results.Ok(platformDto);
        }).Produces<PlatformDto>()
            .Produces(StatusCodes.Status404NotFound);
        
        webApplication.MapPost("/Platform/", (CreatePlatformDto createPlatformDto, IPlatformRepository repository, IMapper mapper) =>
        {
            var platform = mapper.Map<Platform>(createPlatformDto);
            repository.CreatePlatform(platform);
            repository.SaveChanges();
            var platformDto = mapper.Map<PlatformDto>(platform);
            return Results.Created($"/Platform/id/{platformDto.Id}", platformDto);
        }).Produces<Platform>(StatusCodes.Status201Created);
    }   
}