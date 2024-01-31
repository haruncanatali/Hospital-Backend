using AutoMapper;
using Hospital.Application.Common.Mappings;
using Hospital.Domain.Identity;

namespace Hospital.Application.Roles.Queries.Dtos;

public class RoleDto : IMapFrom<Role>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleDto>();
    }
}