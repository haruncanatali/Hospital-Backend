using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Appointments.Queries.Dtos;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Appointments.Queries.GetAppointment;

public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, BaseResponseModel<AppointmentDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAppointmentQueryHandler> _logger;

    public GetAppointmentQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetAppointmentQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<AppointmentDto>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        AppointmentDto? appointment = await _context.Appointments
            .Where(c => c.Id == request.Id)
            .Include(c => c.Category)
            .Include(c => c.Staff)
            .Include(c => c.WorkCalendar)
            .Include(c => c.Patient)
            .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        _logger.LogCritical($"Tek randevu görüntüleme girişimi yapıldı.(ID={request.Id})");
        return BaseResponseModel<AppointmentDto>.Success(appointment, $"Tek randevu görüntüleme girişimi yapıldı.(ID={request.Id})");
    }
}