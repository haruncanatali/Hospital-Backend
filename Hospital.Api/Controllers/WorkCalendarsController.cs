using Hospital.Application.Common.Models;
using Hospital.Application.WorkCalendars.Commands.Create;
using Hospital.Application.WorkCalendars.Commands.Delete;
using Hospital.Application.WorkCalendars.Commands.Update;
using Hospital.Application.WorkCalendars.Queries.Dtos;
using Hospital.Application.WorkCalendars.Queries.GetWorkCalendar;
using Hospital.Application.WorkCalendars.Queries.GetWorkCalendars;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Authorize(Roles = "Admin")]
public class WorkCalendarsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetWorkCalendarsVm>> List([FromQuery] DateTime? date, long categoryId = 0, long staffId = 0)
    {
        return Ok(await Mediator.Send(new GetWorkCalendarsQuery
        {
            Date = date,
            CategoryId = categoryId,
            StaffId = staffId
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkCalendarDto>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetWorkCalendarQuery { Id = id }));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromForm] CreateWorkCalendarCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromForm] UpdateWorkCalendarCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeleteWorkCalendarCommand { Id = id });
        return NoContent();
    }
}