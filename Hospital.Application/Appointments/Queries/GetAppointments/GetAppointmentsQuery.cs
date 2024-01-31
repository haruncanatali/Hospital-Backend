using Hospital.Application.Common.Models;
using MediatR;

namespace Hospital.Application.Appointments.Queries.GetAppointments;

public class GetAppointmentsQuery : IRequest<BaseResponseModel<GetAppointmentsVm>>
{
    public string? StaffIdentityNumber { get; set; }
    public string? PatientIdentityNumber { get; set; }
}