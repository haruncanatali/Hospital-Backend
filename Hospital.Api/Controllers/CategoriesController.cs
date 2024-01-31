using Hospital.Application.Categories.Commands.Create;
using Hospital.Application.Categories.Commands.Delete;
using Hospital.Application.Categories.Commands.Update;
using Hospital.Application.Categories.Queries.Dtos;
using Hospital.Application.Categories.Queries.GetCategories;
using Hospital.Application.Categories.Queries.GetCategory;
using Hospital.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

public class CategoriesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetCategoriesVm>> List([FromQuery] string? name)
    {
        return Ok(await Mediator.Send(new GetCategoriesQuery
        {
            Name = name
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(long id)
    {
        return Ok(await Mediator.Send(new GetCategoryQuery { Id = id }));
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Create([FromForm] CreateCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromForm] UpdateCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Delete(long id)
    {
        await Mediator.Send(new DeleteCategoryCommand { Id = id });
        return NoContent();
    }
}