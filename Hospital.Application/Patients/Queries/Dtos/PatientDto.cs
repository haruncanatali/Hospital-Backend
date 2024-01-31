using AutoMapper;
using Hospital.Application.Appointments.Queries.Dtos;
using Hospital.Application.Common.Mappings;
using Hospital.Application.Common.Models;
using Hospital.Application.Users.Queries.GetUsersList;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace Hospital.Application.Patients.Queries.Dtos;

public class PatientDto : BaseDto, IMapFrom<Patient>
{
    public string Nationality { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
    public int Discontinuity { get; set; }
    public UserDto User { get; set; }
    public List<AppointmentDto> Appointments { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.User, opt =>
                opt.MapFrom(c => c.User))
            .ForMember(dest => dest.Appointments, opt => 
                opt.MapFrom(c=>c.Appointments))
            .ForMember(dest => dest.StatusText, opt =>
                opt.MapFrom(c => c.Status == EntityStatus.Active ? "Active" : "Passive"));
    }
}