using Hospital.Application.Appointments.Commands.Create;
using Hospital.Application.Common.Models.Queue;
using Hospital.BackgroundApp.Services;
using MediatR;

namespace Hospital.BackgroundApp.Consumers;

public class AppointmentDeclarationService : IAppointmentDeclarationService
{
    private readonly IMediator _mediator;

    public AppointmentDeclarationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task AddAppointment(AddAppointmentRequestModel model)
    {
        try
        {
            await _mediator.Send(new CreateAppointmentCommand
            {
                CategoryId = model.CategoryId,
                PatientId = model.PatientId,
                StaffId = model.StaffId,
                WorkCalendarId = model.WorkCalendarId
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }
    }
}