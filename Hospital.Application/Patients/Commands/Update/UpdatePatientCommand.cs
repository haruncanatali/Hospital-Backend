using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Patients.Commands.Update;

public class UpdatePatientCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Nationality { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
    
    public class Handler : IRequestHandler<UpdatePatientCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<UpdatePatientCommand> _logger;

        public Handler(IApplicationContext context, ILogger<UpdatePatientCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await _context.Patients
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (patient != null)
            {
                patient.Nationality = request.Nationality;
                patient.Notes = request.Notes;
                patient.Address = request.Address;
                _context.Patients.Update(patient);
                await _context.SaveChangesAsync(cancellationToken);
                
                _logger.LogCritical($"Hasta başarıyla güncellendi. ID:{request.Id}");
                return BaseResponseModel<Unit>.Success(Unit.Value,$"Hasta başarıyla güncellendi. ID:{request.Id}");
            }

            _logger.LogCritical($"Güncellenecek hasta bulunamadı. ID:{request.Id}");
            throw new BadRequestException($"Güncellenecek hasta bulunamadı. ID:{request.Id}");
        }
    }
}