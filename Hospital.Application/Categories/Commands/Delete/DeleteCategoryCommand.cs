using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Category = Hospital.Domain.Entities.Category;

namespace Hospital.Application.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteCategoryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<DeleteCategoryCommand> _logger;

        public Handler(IApplicationContext context, ILogger<DeleteCategoryCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category != null)
            {
                category.Status = EntityStatus.Passive;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogCritical("Kategori başarıyla silindi.");
                return BaseResponseModel<Unit>.Success(Unit.Value, "Kategori başarıyla silindi.");
            }
            
            _logger.LogCritical($"Silinecek kategori bulunamadı. ID:{request.Id}");
            throw new BadRequestException($"Silinecek kategori bulunamadı. ID:{request.Id}");
        }
    }
}