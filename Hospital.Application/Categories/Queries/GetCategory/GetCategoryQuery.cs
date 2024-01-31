using Hospital.Application.Categories.Queries.Dtos;
using Hospital.Application.Common.Models;
using MediatR;

namespace Hospital.Application.Categories.Queries.GetCategory;

public class GetCategoryQuery : IRequest<BaseResponseModel<CategoryDto>>
{
    public long Id { get; set; }
}