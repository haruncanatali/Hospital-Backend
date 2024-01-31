using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Patients.Commands.Delete;

public class DeletePatientCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeletePatientCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<DeletePatientCommand> _logger;

        public Handler(IApplicationContext context, ILogger<DeletePatientCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await _context.Patients
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (patient != null)
            {
                patient.Status = EntityStatus.Passive;
                _context.Patients.Update(patient);
                _logger.LogCritical($"Hasta başarıyla güncellendi. ID:{request.Id}");
                await _context.SaveChangesAsync(cancellationToken);
            }

            _logger.LogCritical($"Silinecek hasta bulunamadı. ID:{request.Id}");
            throw new Exception($"Silinecek hasta bulunamadı. ID:{request.Id}");
        }
    }
}