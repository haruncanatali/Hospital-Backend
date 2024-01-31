using AutoMapper;
using Hospital.Application.Common.Mappings;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace Hospital.Application.Categories.Queries.Dtos;

public class CategoryPartialDto : BaseDto, IMapFrom<Category>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryPartialDto>()
            .ForMember(dest => dest.StatusText, opt
                => opt.MapFrom(c => c.Status == EntityStatus.Active ? "Active" : "Passive"));
    }
}