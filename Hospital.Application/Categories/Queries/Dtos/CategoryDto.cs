using AutoMapper;
using Hospital.Application.Common.Mappings;
using Hospital.Application.Common.Models;
using Hospital.Application.Staves.Queries.Dtos;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace Hospital.Application.Categories.Queries.Dtos;

public class CategoryDto : BaseDto, IMapFrom<Category>
{
    public string Name { get; set; }
    public List<StaffPartialDto> Staves { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Staves, opt
                => opt.MapFrom(c => c.Staves))
            .ForMember(dest => dest.StatusText, opt
                => opt.MapFrom(c => c.Status == EntityStatus.Active ? "Active" : "Passive"));
    }
}