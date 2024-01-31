using Hospital.Application.Common.Models;
using Hospital.Application.WorkCalendars.Queries.Dtos;
using MediatR;

namespace Hospital.Application.WorkCalendars.Queries.GetWorkCalendar;

public class GetWorkCalendarQuery : IRequest<BaseResponseModel<WorkCalendarDto>>
{
    public long Id { get; set; }
}