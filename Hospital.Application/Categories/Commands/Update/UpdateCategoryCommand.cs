using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Categories.Commands.Update;

public class UpdateCategoryCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public EntityStatus EntityStatus { get; set; }
    
    public class Handler : IRequestHandler<UpdateCategoryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<UpdateCategoryCommand> _logger;

        public Handler(IApplicationContext context, ILogger<UpdateCategoryCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category != null)
            {
                category.Name = request.Name;
                category.Status = request.EntityStatus;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogCritical($"Kategori başarıyla güncellendi. ID:{request.Id}");
                return BaseResponseModel<Unit>.Success(Unit.Value, $"Kategori başarıyla güncellendi. ID:{request.Id}");
            }

            _logger.LogCritical($"Güncellenecek kategori bulunamadı. ID:{request.Id}");
            throw new BadRequestException($"Güncellenecek kategori bulunamadı. ID:{request.Id}");
        }
    }
}