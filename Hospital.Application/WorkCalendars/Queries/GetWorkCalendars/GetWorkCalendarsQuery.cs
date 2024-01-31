using Hospital.Application.Common.Models;
using Hospital.Application.WorkCalendars.Queries.Dtos;
using MediatR;

namespace Hospital.Application.WorkCalendars.Queries.GetWorkCalendars;

public class GetWorkCalendarsQuery : IRequest<BaseResponseModel<GetWorkCalendarsVm>>
{
    public long CategoryId { get; set; }
    public long StaffId { get; set; }
    public DateTime? Date { get; set; }
}