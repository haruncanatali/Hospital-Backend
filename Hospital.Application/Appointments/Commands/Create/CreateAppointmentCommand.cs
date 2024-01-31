using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Helpers;
using Hospital.Application.Common.Hubs;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Appointments.Commands.Create;

public class CreateAppointmentCommand : IRequest<BaseResponseModel<Unit>>
{
    public long CategoryId { get; set; }
    public long StaffId { get; set; }
    public long PatientId { get; set; }
    public long WorkCalendarId { get; set; }
    
    public class Handler : IRequestHandler<CreateAppointmentCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<CreateAppointmentCommand> _logger;
        private IHubContext<AppointmentHub> _appointmentHub;

        public Handler(IApplicationContext context, ILogger<CreateAppointmentCommand> logger, IHubContext<AppointmentHub> appointmentHub)
        {
            _context = context;
            _logger = logger;
            _appointmentHub = appointmentHub;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Category? category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

                if (category == null)
                {
                    _logger.LogCritical("Başarısız randevu eklenme girişimi: Kategori bulunamadı.");
                    throw new BadRequestException("Başarısız randevu eklenme girişimi: Kategori bulunamadı.");
                }

                Staff? staff = await _context.Staves
                    .Where(c => c.Id == request.StaffId)
                    .Include(c=>c.User)
                    .FirstOrDefaultAsync(cancellationToken);

                if (staff == null)
                {
                    _logger.LogCritical("Başarısız randevu eklenme girişimi: Personel bulunamadı.");
                    throw new BadRequestException("Başarısız randevu eklenme girişimi: Personel bulunamadı.");
                }

                Patient? patient = await _context.Patients
                    .Where(c => c.Id == request.PatientId)
                    .Include(c=>c.User)
                    .FirstOrDefaultAsync(cancellationToken);

                if (patient == null)
                {
                    _logger.LogCritical("Başarısız randevu eklenme girişimi: Hasta bulunamadı.");
                    throw new BadRequestException("Başarısız randevu eklenme girişimi: Hasta bulunamadı.");
                }

                WorkCalendar? workCalendar = await _context.WorkCalendars
                    .FirstOrDefaultAsync(c => c.Id == request.WorkCalendarId, cancellationToken);

                if (workCalendar == null)
                {
                    _logger.LogCritical("Başarısız randevu eklenme girişimi: İş tarihi bulunamadı.");
                    return BaseResponseModel<Unit>.FailureSingle(
                        "Başarısız randevu eklenme girişimi: İş tarihi bulunamadı.");
                    throw new BadRequestException("Başarısız randevu eklenme girişimi: İş tarihi bulunamadı.");
                }

                bool isBusy = await _context.Appointments
                    .AnyAsync(c => c.WorkCalendarId == workCalendar.Id, cancellationToken);

                if (isBusy)
                {
                    _logger.LogCritical("Başarısız randevu eklenme girişimi: Bu randevu tarihi doludur.");
                    throw new BadRequestException("Başarısız randevu eklenme girişimi: Bu randevu tarihi doludur.");
                }

                await _context.Appointments.AddAsync(new Appointment
                {
                    CategoryId = request.CategoryId,
                    StaffId = request.StaffId,
                    PatientId = request.PatientId,
                    WorkCalendarId = request.WorkCalendarId
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            
                _logger.LogCritical("Randevu başarılı bir şekilde oluşturuldu.");

                string realtimeMessage =
                    $"{workCalendar.Date.Date}-{workCalendar.Date.Hour}:{workCalendar.Date.Minute} tarihinde " +
                    $"{patient.User.FullName} tarafından " +
                    $"{category.Name} bölümü personellerinden " +
                    $"{staff.Title.GetDescription() + " " + staff.User.FullName} üzerine" +
                    "randevu oluşturdu.";

                await _appointmentHub.Clients.All.SendAsync("dbEventMessage",
                    realtimeMessage, 
                    cancellationToken: cancellationToken);
                return BaseResponseModel<Unit>.FailureSingle(realtimeMessage);
            }
            catch (BadRequestException e)
            {
                await _appointmentHub.Clients.All.SendAsync("dbEventMessage",
                    $"Randevu oluşturulurken hata meydana geldi. {e.Message}", 
                    cancellationToken: cancellationToken);
                _logger.LogCritical($"Randevu oluşturulurken hata meydana geldi. {e.Message}");
                return BaseResponseModel<Unit>.FailureSingle($"Randevu oluşturulurken hata meydana geldi. {e.Message}");
            }
        }
    }
}