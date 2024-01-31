using AutoMapper;
using Hospital.Application.Categories.Queries.Dtos;
using Hospital.Application.Common.Mappings;
using Hospital.Application.Common.Models;
using Hospital.Application.Patients.Queries.Dtos;
using Hospital.Application.Staves.Queries.Dtos;
using Hospital.Application.WorkCalendars.Queries.Dtos;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace Hospital.Application.Appointments.Queries.Dtos;

public class AppointmentDto : BaseDto, IMapFrom<Appointment>
{
    public CategoryPartialDto Category { get; set; }
    public StaffDto Staff { get; set; }
    public PatientDto Patient { get; set; }
    public WorkCalendarDto WorkCalendar { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.Category, opt =>
                opt.MapFrom(c=>c.Category))
            .ForMember(dest => dest.Staff, opt =>
                opt.MapFrom(c=>c.Staff))
            .ForMember(dest => dest.Patient, opt =>
                opt.MapFrom(c=>c.Patient))
            .ForMember(dest => dest.WorkCalendar, opt =>
                opt.MapFrom(c=>c.WorkCalendar))
            .ForMember(dest => dest.StatusText, opt
                => opt.MapFrom(c => c.Status == EntityStatus.Active ? "Active" : "Passive"));
    }
}