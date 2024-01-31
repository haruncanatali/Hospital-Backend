using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.WorkCalendars.Commands.Delete;

public class DeleteWorkCalendarCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteWorkCalendarCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<DeleteWorkCalendarCommand> _logger;

        public Handler(IApplicationContext context, ILogger<DeleteWorkCalendarCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteWorkCalendarCommand request, CancellationToken cancellationToken)
        {
            WorkCalendar? workCalendar = await _context.WorkCalendars
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (workCalendar == null)
            {
                _logger.LogCritical($"İş günü bulunamadı. ID:{request.Id}");
                throw new BadRequestException($"İş günü bulunamadı. ID:{request.Id}");
            }

            workCalendar.Status = EntityStatus.Passive;
            _context.WorkCalendars.Update(workCalendar);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogCritical("İş günü başarıyla silindi.");
            return BaseResponseModel<Unit>.Success(Unit.Value,"İş günü başarıyla silindi.");
        }
    }
}