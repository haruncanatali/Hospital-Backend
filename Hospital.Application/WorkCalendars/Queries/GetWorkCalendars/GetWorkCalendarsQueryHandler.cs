using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Application.WorkCalendars.Queries.Dtos;
using Hospital.Application.WorkCalendars.Queries.GetWorkCalendar;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.WorkCalendars.Queries.GetWorkCalendars;

public class GetWorkCalendarsQueryHandler : IRequestHandler<GetWorkCalendarsQuery, BaseResponseModel<GetWorkCalendarsVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetWorkCalendarsQueryHandler> _logger;

    public GetWorkCalendarsQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetWorkCalendarsQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<GetWorkCalendarsVm>> Handle(GetWorkCalendarsQuery request, CancellationToken cancellationToken)
    {
        var workCalendarsQuery = _context.WorkCalendars.AsQueryable();

        if (request.StaffId != 0)
        {
            workCalendarsQuery = workCalendarsQuery.Where(c => c.StaffId == request.StaffId);
        }

        if (request.CategoryId != 0)
        {
            workCalendarsQuery = workCalendarsQuery.Where(c => c.CategoryId == request.CategoryId);
        }

        if (request.Date != null)
        {
            workCalendarsQuery = workCalendarsQuery.Where(c => c.Date.Date == request.Date.Value.Date);
        }

        List<WorkCalendarDto> workCalendars = await workCalendarsQuery
            .Include(c => c.Staff)
            .Include(c => c.Category)
            .Include(c => c.Appointments)
            .ProjectTo<WorkCalendarDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        _logger.LogCritical("Çoklu iş günü görüntüleme girişiminde bulunuldu.");
        return BaseResponseModel<GetWorkCalendarsVm>.Success(new GetWorkCalendarsVm
        {
            WorkCalendars = workCalendars,
            Count = workCalendars.Count
        });
    }
}