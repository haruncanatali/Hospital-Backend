using AutoMapper;
using Hospital.Application.Common.Helpers;
using Hospital.Application.Common.Mappings;
using Hospital.Application.Common.Models;
using Hospital.Application.Users.Queries.GetUsersList;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace Hospital.Application.Staves.Queries.Dtos;

public class StaffPartialDto : BaseDto, IMapFrom<Staff>
{
    public string Resume { get; set; }
    public double Score { get; set; }
    public Title Title { get; set; }
    public string TitleText { get; set; }
    public UserDto User { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Staff, StaffPartialDto>()
            .ForMember(dest => dest.User, opt =>
                opt.MapFrom(c => c.User))
            .ForMember(dest => dest.TitleText, opt =>
                opt.MapFrom(c=>c.Title.GetDescription()))
            .ForMember(dest => dest.StatusText, opt
                => opt.MapFrom(c => c.Status == EntityStatus.Active ? "Active" : "Passive"));
    }
}