using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.WorkCalendars.Commands.Update;

public class UpdateWorkCalendarCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public long CategoryId { get; set; }
    public long StaffId { get; set; }
    
    public class Handler : IRequestHandler<UpdateWorkCalendarCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<UpdateWorkCalendarCommand> _logger;

        public Handler(IApplicationContext context, ILogger<UpdateWorkCalendarCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateWorkCalendarCommand request, CancellationToken cancellationToken)
        {
            WorkCalendar? workCalendar = await _context.WorkCalendars
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (workCalendar == null)
            {
                _logger.LogCritical($"Güncellenmek istenen iş günü bulunamadı. ID:{request.Id}");
                throw new BadRequestException($"Silinmek istenen iş günü bulunamadı. ID:{request.Id}");
            }
            
            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (category == null)
            {
                _logger.LogCritical($"İş günü güncellemesi için ilgili kategori bulunamadı. ID:{request.CategoryId}");
                throw new BadRequestException($"İş günü güncellemesi için ilgili kategori bulunamadı. ID:{request.CategoryId}");
            }

            Staff? staff = await _context.Staves
                .FirstOrDefaultAsync(c => c.Id == request.StaffId, cancellationToken);

            if (staff == null)
            {
                _logger.LogCritical($"İş günü güncellemesi için ilgili personel bulunamadı. ID:{request.StaffId}");
                throw new BadRequestException($"İş günü güncellemesi için ilgili personel bulunamadı. ID:{request.StaffId}");
            }

            workCalendar.Date = request.Date;
            workCalendar.CategoryId = request.CategoryId;
            workCalendar.StaffId = request.StaffId;

            _context.WorkCalendars.Update(workCalendar);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogCritical($"İş günü başarıyla güncellendi. ID:{request.Id}");
            return BaseResponseModel<Unit>.Success(Unit.Value, $"İş günü başarıyla güncellendi. ID:{request.Id}");
        }
    }
}