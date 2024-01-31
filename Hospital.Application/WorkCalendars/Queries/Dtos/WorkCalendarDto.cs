using AutoMapper;
using Hospital.Application.Appointments.Queries.Dtos;
using Hospital.Application.Categories.Queries.Dtos;
using Hospital.Application.Common.Mappings;
using Hospital.Application.Common.Models;
using Hospital.Application.Staves.Queries.Dtos;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace Hospital.Application.WorkCalendars.Queries.Dtos;

public class WorkCalendarDto : BaseDto, IMapFrom<WorkCalendar>
{
    public DateTime Date { get; set; }
    public CategoryPartialDto Category { get; set; }
    public StaffPartialDto Staff { get; set; }
    public bool HasAppointment { get; set; }
    public long AppointmentId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WorkCalendar, WorkCalendarDto>()
            .ForMember(dest => dest.Category, opt =>
                opt.MapFrom(c=>c.Category))
            .ForMember(dest => dest.Staff, opt =>
                opt.MapFrom(c=>c.Staff))
            .ForMember(dest => dest.HasAppointment, opt =>
                opt.MapFrom(c=>c.Appointments.Count > 0))
            .ForMember(dest => dest.AppointmentId, opt =>
                opt.MapFrom(c=>c.Appointments.Count > 0 ? c.Appointments.First().Id : 0))
            .ForMember(dest => dest.StatusText, opt
                => opt.MapFrom(c => c.Status == EntityStatus.Active ? "Active" : "Passive"));;
    }
}