using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Appointments.Queries.Dtos;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Appointments.Queries.GetAppointments;

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, BaseResponseModel<GetAppointmentsVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAppointmentsQueryHandler> _logger;

    public GetAppointmentsQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetAppointmentsQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<GetAppointmentsVm>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        List<AppointmentDto> appointments = await _context.Appointments
            .Include(c => c.Category)
            .Include(c => c.Staff)
            .ThenInclude(c => c.User)
            .Include(c => c.WorkCalendar)
            .Include(c => c.Patient)
            .ThenInclude(c => c.User)
            .Where(c =>
                (request.StaffIdentityNumber == null ||
                 c.Staff.User.IdentityNumber.Contains(request.StaffIdentityNumber)) &&
                (request.PatientIdentityNumber == null ||
                 c.Patient.User.IdentityNumber.Contains(request.PatientIdentityNumber)))
            .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        _logger.LogCritical("Çoklu randevu çekme girişimi yapıldı.");
        return BaseResponseModel<GetAppointmentsVm>.Success(new GetAppointmentsVm
        {
            Appointments = appointments,
            Count = appointments.Count
        }, "Çoklu randevu çekme girişimi yapıldı.");
    }
}