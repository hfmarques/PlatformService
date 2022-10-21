using AutoMapper;
using PlatformService.Dto;
using PlatformService.Models;

namespace PlatformService;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatePlatformDto, Platform>();
        CreateMap<Platform, PlatformReadDto>();
    }
}