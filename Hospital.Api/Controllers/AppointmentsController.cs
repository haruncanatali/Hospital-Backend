using Hospital.Application.Appointments.Commands.Create;
using Hospital.Application.Appointments.Commands.CreateWithQueue;
using Hospital.Application.Appointments.Commands.Delete;
using Hospital.Application.Appointments.Queries.Dtos;
using Hospital.Application.Appointments.Queries.GetAppointments;
using Hospital.Application.Common.Models;
using Hospital.Application.Patients.Queries.GetPatient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

public class AppointmentsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetAppointmentsVm>> List([FromQuery] string? staffIdentityNumber, string? patientIdentityNumber)
    {
        return Ok(await Mediator.Send(new GetAppointmentsQuery
        {
            StaffIdentityNumber = staffIdentityNumber,
            PatientIdentityNumber = patientIdentityNumber
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel<AppointmentDto>>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetPatientQuery { Id = id }));
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromForm] CreateAppointmentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPost]
    [Route("CreateAppointmentWithQueue")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> CreateWithQueue([FromForm] CreateAppointmentWithQueueCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeleteAppointmentCommand { Id = id });
        return NoContent();
    }
}