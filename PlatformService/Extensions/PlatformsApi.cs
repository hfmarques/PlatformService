using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dto;
using PlatformService.Models;

namespace PlatformService.Extensions;

public static class PlatformsApi
{
    public static void PlatformApi(this WebApplication webApplication)
    {
        webApplication.MapGet("/Platform/", (IPlatformRepository repository, IMapper mapper) =>
        {
            var platforms = repository.GetAll();
            var platformsDto = mapper.Map<List<PlatformReadDto>>(platforms);
            return Results.Ok(platformsDto);
        }).Produces<PlatformReadDto>();
        
        webApplication.MapGet("/Platform/id/{id}", (int id, IPlatformRepository repository, IMapper mapper) =>
        {
            var platform = repository.GetById(id);
            if(platform is null)
                return Results.NotFound();

            var platformDto = mapper.Map<PlatformReadDto>(platform);
            
            return Results.Ok(platformDto);
        }).Produces<PlatformReadDto>()
            .Produces(StatusCodes.Status404NotFound);
        
        webApplication.MapPost("/Platform/", (CreatePlatformDto createPlatformDto, IPlatformRepository repository, IMapper mapper) =>
        {
            var platform = mapper.Map<Platform>(createPlatformDto);
            repository.CreatePlatform(platform);
            repository.SaveChanges();
            var platformDto = mapper.Map<PlatformReadDto>(platform);
            return Results.Created($"/Platform/id/{platformDto.Id}", platformDto);
        }).Produces<Platform>(StatusCodes.Status201Created);
    }   
}