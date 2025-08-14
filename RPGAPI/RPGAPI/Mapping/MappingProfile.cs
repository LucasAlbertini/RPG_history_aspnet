using AutoMapper;
using RPGAPI.Core.Entities;
using RPGAPI.Models.DTOs;
namespace RPGAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Faction, FactionDto>();
                //.ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Goals))
                //.ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.Resources));
            CreateMap<Goal, GoalDto>();
                //.ForMember(dest => dest.FactionId, opt => opt.MapFrom(src => src.FactionId));
            CreateMap<FactionDto, Faction>();
                //.ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Goals))
                //.ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.Resources));
            CreateMap<GoalDto, Goal>();
                //.ForMember(dest => dest.FactionId, opt => opt.MapFrom(src => src.FactionId));
        }
    }
}
