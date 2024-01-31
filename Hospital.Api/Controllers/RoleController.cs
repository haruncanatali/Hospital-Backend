using Hospital.Application.Common.Models;
using Hospital.Application.Roles.Commands.AddToRole;
using Hospital.Application.Roles.Commands.Create;
using Hospital.Application.Roles.Commands.Update;
using Hospital.Application.Roles.Queries.Dtos;
using Hospital.Application.Roles.Queries.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

public class RoleController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<RoleDto>>>> List([FromQuery]string? name)
    {
        return Ok(await Mediator.Send(new GetRolesQuery
        {
            Name = name
        }));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create(CreateRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPost]
    [Route("AddToRole")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddToRole(AddToRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromForm] UpdateRoleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}