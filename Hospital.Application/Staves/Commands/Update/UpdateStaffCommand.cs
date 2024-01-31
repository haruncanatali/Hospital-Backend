using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Application.Staves.Commands.Delete;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Staves.Commands.Update;

public class UpdateStaffCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Resume { get; set; }
    public Title Title { get; set; }
    public long CategoryId { get; set; }
    
    public class Handler : IRequestHandler<UpdateStaffCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<DeleteStaffCommand> _logger;

        public Handler(IApplicationContext context, ILogger<DeleteStaffCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            Staff? staff = await _context.Staves
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (staff != null && category != null)
            {
                staff.Resume = request.Resume;
                staff.Title = request.Title;
                staff.CategoryId = request.CategoryId;

                _context.Staves.Update(staff);
                await _context.SaveChangesAsync(cancellationToken);
                
                _logger.LogCritical($"Personel başarıyla güncellendi. ID:{request.Id}");
                return BaseResponseModel<Unit>.Success(Unit.Value, $"Personel başarıyla güncellendi. ID:{request.Id}");
            }

            if (category == null)
            {
                _logger.LogCritical($"Güncellenecek personel için ilgili kategori bulunamadı. ID:{request.CategoryId}");
                throw new BadRequestException($"Güncellenecek personel için ilgili kategori bulunamadı. ID:{request.CategoryId}");
            }

            _logger.LogCritical($"Güncellecek personel bulunamadı. ID:{request.Id}");
            throw new BadRequestException($"Güncellecek personel bulunamadı. ID:{request.Id}");
        }
    }
}