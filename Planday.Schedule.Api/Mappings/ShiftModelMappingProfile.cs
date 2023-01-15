using AutoMapper;
using Planday.Schedule.Api.Models;

namespace Planday.Schedule.Api.Mappings
{
    public class ShiftModelMappingProfile : Profile
    {
        public ShiftModelMappingProfile()
        {
            CreateMap<ShiftModel, Shift>()
                .ForMember(dst => dst.Start, opt => opt.MapFrom(dest => dest.Start))
                .ForMember(dst => dst.End, opt => opt.MapFrom(dest => dest.End));
        }
    }
}