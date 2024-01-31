using Hospital.Application.Common.Models;
using Hospital.Application.Roles.Queries.Dtos;
using MediatR;

namespace Hospital.Application.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<BaseResponseModel<List<RoleDto>>>
{
    public string? Name { get; set; }
}