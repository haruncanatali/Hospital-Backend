using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Categories.Commands.Create;

public class CreateCategoryCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    
    public class Handler : IRequestHandler<CreateCategoryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<CreateCategoryCommand> _logger;

        public Handler(IApplicationContext context, ILogger<CreateCategoryCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(new Category
            {
                Name = request.Name
            }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogCritical($"Eklenen Kategori : {request.Name}");

            return BaseResponseModel<Unit>.Success(Unit.Value, "Success");
        }
    }
}