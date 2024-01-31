using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Staves.Commands.Delete;

public class DeleteStaffCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteStaffCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<DeleteStaffCommand> _logger;

        public Handler(IApplicationContext context, ILogger<DeleteStaffCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            Staff? staff = await _context.Staves
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (staff != null)
            {
                staff.Status = EntityStatus.Passive;
                _context.Staves.Update(staff);
                await _context.SaveChangesAsync(cancellationToken);
                
                _logger.LogCritical($"Personel başarıyla silindi. ID:{request.Id}");
                return BaseResponseModel<Unit>.Success(Unit.Value, "Success");
            }
            
            _logger.LogCritical($"Silinecek personel bulunamadı. ID:{request.Id}");
            throw new BadRequestException($"Silinecek personel bulunamadı. ID:{request.Id}");
        }
    }
}