using Hospital.Application.Patients.Queries.Dtos;

namespace Hospital.Application.Patients.Queries.GetPatients;

public class GetPatientsVm
{
    public List<PatientDto> Patients { get; set; }
    public int Count { get; set; }
}