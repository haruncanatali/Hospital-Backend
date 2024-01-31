using Hospital.Application.Common.Models;
using Hospital.Application.Staves.Queries.Dtos;
using MediatR;

namespace Hospital.Application.Staves.Queries.GetStaff;

public class GetStaffQuery : IRequest<BaseResponseModel<StaffDto>>
{
    public long Id { get; set; }
}