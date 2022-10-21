using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PlatformCreateDto, Platform>();
        CreateMap<Platform, PlatformReadDto>();
    }
}