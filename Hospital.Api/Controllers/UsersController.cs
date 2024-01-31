using Hospital.Application.Common.Models;
using Hospital.Application.Users.Commands.CreateUser;
using Hospital.Application.Users.Commands.DeleteRoleFromUser;
using Hospital.Application.Users.Commands.DeleteUser;
using Hospital.Application.Users.Commands.UpdateUser;
using Hospital.Application.Users.Queries.GetUserDetail;
using Hospital.Application.Users.Queries.GetUsersList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

public class UsersController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<UserListVm>> GetAll([FromQuery] string? identityNumber)
    {
        return Ok(await Mediator.Send(new GetUserListQuery
        {
            IdentityNumber = identityNumber
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserListVm>> GetById(long id = 0)
    {
        return Ok(await Mediator.Send(new UserDetailQuery { Id = id }));
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Create([FromForm] CreateUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Update([FromForm] UpdateUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [Route("DeleteRoleFromUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Update([FromForm] DeleteRoleFromUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteUserCommand { Id = id });
        return NoContent();
    }
}