using Hospital.Application.Common.Models;
using MediatR;

namespace Hospital.Application.Staves.Queries.GetStaves;

public class GetStavesQuery : IRequest<BaseResponseModel<GetStavesVm>>
{
    public string? IdentityNumber { get; set; }
}