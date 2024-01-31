using Hospital.Application.Common.Models;
using Hospital.Application.Patients.Queries.Dtos;
using MediatR;

namespace Hospital.Application.Patients.Queries.GetPatient;

public class GetPatientQuery : IRequest<BaseResponseModel<PatientDto>>
{
    public long Id { get; set; }
}