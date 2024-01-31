using Hospital.Application.WorkCalendars.Queries.Dtos;

namespace Hospital.Application.WorkCalendars.Queries.GetWorkCalendars;

public class GetWorkCalendarsVm
{
    public List<WorkCalendarDto> WorkCalendars { get; set; }
    public int Count { get; set; }
}