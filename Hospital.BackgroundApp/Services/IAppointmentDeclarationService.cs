using Hospital.Application.Common.Models.Queue;

namespace Hospital.BackgroundApp.Services;

public interface IAppointmentDeclarationService
{
    Task AddAppointment(AddAppointmentRequestModel model);
}