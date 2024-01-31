using Hospital.Application.Common.Models;
using MediatR;

namespace Hospital.Application.Patients.Queries.GetPatients;

public class GetPatientsQuery : IRequest<BaseResponseModel<GetPatientsVm>>
{
    public string? IdentityNumber { get; set; }
}