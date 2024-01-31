using Hospital.Application.Common.Models;
using Hospital.Application.Patients.Commands.Create;
using Hospital.Application.Patients.Commands.Delete;
using Hospital.Application.Patients.Commands.Update;
using Hospital.Application.Patients.Queries.Dtos;
using Hospital.Application.Patients.Queries.GetPatient;
using Hospital.Application.Patients.Queries.GetPatients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

public class PatientsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetPatientsVm>> List([FromQuery] string? identityNumber)
    {
        return Ok(await Mediator.Send(new GetPatientsQuery
        {
            IdentityNumber = identityNumber
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDto>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetPatientQuery { Id = id }));
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromForm] CreatePatientCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromForm] UpdatePatientCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeletePatientCommand() { Id = id });
        return NoContent();
    }
}