using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.WorkCalendars.Commands.Create;

public class CreateWorkCalendarCommand : IRequest<BaseResponseModel<Unit>>
{
    public DateTime Date { get; set; }
    public long CategoryId { get; set; }
    public long StaffId { get; set; }
    
    public class Handler : IRequestHandler<CreateWorkCalendarCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<CreateWorkCalendarCommand> _logger;

        public Handler(IApplicationContext context, ILogger<CreateWorkCalendarCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateWorkCalendarCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (category == null)
            {
                _logger.LogCritical($"İş günü için eklenmek istenen kategori bulunamadı. ID:{request.CategoryId}");
                throw new BadRequestException($"Randevu için eklenmek istenen kategori bulunamadı. ID:{request.CategoryId}");
            }

            Staff? staff = await _context.Staves
                .FirstOrDefaultAsync(c => c.Id == request.StaffId, cancellationToken);

            if (staff == null)
            {
                _logger.LogCritical($"İş günü için eklenmek istenen personel bulunamadı. ID:{request.StaffId}");
                throw new BadRequestException($"Randevu için eklenmek istenen personel bulunamadı. ID:{request.StaffId}");
            }

            await _context.WorkCalendars.AddAsync(new WorkCalendar
            {
                Date = request.Date,
                CategoryId = request.CategoryId,
                StaffId = request.StaffId
            });
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogCritical("İş günü başarıyla oluşturuldu.");
            return BaseResponseModel<Unit>.Success(Unit.Value, "İş günü başarıyla oluşturuldu.");
        }
    }
}