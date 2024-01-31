using Hospital.Application.Appointments.Queries.Dtos;

namespace Hospital.Application.Appointments.Queries.GetAppointments;

public class GetAppointmentsVm
{
    public List<AppointmentDto> Appointments { get; set; }
    public int Count { get; set; }
}