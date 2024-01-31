using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Application.WorkCalendars.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.WorkCalendars.Queries.GetWorkCalendar;

public class GetWorkCalendarQueryHandler : IRequestHandler<GetWorkCalendarQuery, BaseResponseModel<WorkCalendarDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetWorkCalendarQueryHandler> _logger;

    public GetWorkCalendarQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetWorkCalendarQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<WorkCalendarDto>> Handle(GetWorkCalendarQuery request, CancellationToken cancellationToken)
    {
        WorkCalendarDto? workCalendar = await _context.WorkCalendars
            .Where(c => c.Id == request.Id)
            .Include(c => c.Staff)
            .Include(c => c.Appointments)
            .Include(c => c.Category)
            .ProjectTo<WorkCalendarDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        _logger.LogCritical($"Tekil iş günü görüntüleme girişiminde bulunuldu. ID:{request.Id}");
        return BaseResponseModel<WorkCalendarDto>.Success(workCalendar, $"Tekil iş günü görüntüleme girişiminde bulunuldu. ID:{request.Id}");
    }
}