using Hospital.Application.Staves.Queries.Dtos;

namespace Hospital.Application.Staves.Queries.GetStaves;

public class GetStavesVm
{
    public List<StaffDto> Staves { get; set; }
    public int Count { get; set; }
}