using AutoMapper;
using Hospital.Application.Common.Mappings;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace Hospital.Application.Appointments.Queries.Dtos;

public class AppointmentPartialDto : BaseDto, IMapFrom<Appointment>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Appointment, AppointmentPartialDto>()
            .ForMember(dest => dest.StatusText, opt
                => opt.MapFrom(c => c.Status == EntityStatus.Active ? "Active" : "Passive"));
    }
}