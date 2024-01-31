using Hospital.Application.Appointments.Queries.Dtos;
using Hospital.Application.Common.Models;
using MediatR;

namespace Hospital.Application.Appointments.Queries.GetAppointment;

public class GetAppointmentQuery : IRequest<BaseResponseModel<AppointmentDto>>
{
    public long Id { get; set; }
}