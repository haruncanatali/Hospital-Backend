using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Appointments.Commands.Delete;

public class DeleteAppointmentCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteAppointmentCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<DeleteAppointmentCommand> _logger;

        public Handler(IApplicationContext context, ILogger<DeleteAppointmentCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _context.Appointments
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync(cancellationToken);
                
                _logger.LogCritical("Randevu başarıyla silindi.");
                return BaseResponseModel<Unit>.Success(Unit.Value,"Randevu başarıyla silindi.");
            }
            
            _logger.LogCritical("Başarısız randevu silme girişimi: Silinmek istenen randevu bulunamadı.");
            throw new BadRequestException("Başarısız randevu silme girişimi: Silinmek istenen randevu bulunamadı.");
        }
    }
}