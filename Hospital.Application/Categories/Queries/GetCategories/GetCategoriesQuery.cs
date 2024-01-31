using Hospital.Application.Common.Models;
using MediatR;

namespace Hospital.Application.Categories.Queries.GetCategories;

public class GetCategoriesQuery : IRequest<BaseResponseModel<GetCategoriesVm>>
{
    public string? Name { get; set; }
}