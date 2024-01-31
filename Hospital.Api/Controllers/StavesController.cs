using Hospital.Application.Common.Models;
using Hospital.Application.Staves.Commands.Create;
using Hospital.Application.Staves.Commands.Delete;
using Hospital.Application.Staves.Commands.Update;
using Hospital.Application.Staves.Queries.Dtos;
using Hospital.Application.Staves.Queries.GetStaff;
using Hospital.Application.Staves.Queries.GetStaves;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

public class StavesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetStavesVm>> List([FromQuery] string? identityNumber)
    {
        return Ok(await Mediator.Send(new GetStavesQuery
        {
            IdentityNumber = identityNumber
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StaffDto>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetStaffQuery { Id = id }));
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromForm] CreateStaffCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromForm] UpdateStaffCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeleteStaffCommand { Id = id });
        return NoContent();
    }
}