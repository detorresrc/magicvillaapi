using AutoMapper;
using MagicVilla.Models;
using MagicVilla.Models.DTOs;

namespace MagicVilla.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<VillaDTO, Villa>().ReverseMap();
        CreateMap<VillaCreateDTO, Villa>().ReverseMap();
        CreateMap<VillaUpdateDTO, Villa>().ReverseMap();
    }
}