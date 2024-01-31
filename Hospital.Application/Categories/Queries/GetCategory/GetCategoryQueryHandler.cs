using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Categories.Queries.Dtos;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, BaseResponseModel<CategoryDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCategoryQueryHandler> _logger;

    public GetCategoryQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetCategoryQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        CategoryDto? category = await _context.Categories
            .Where(c => c.Id == request.Id)
            .Include(c => c.Staves)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        _logger.LogCritical($"Tek kategori görüntüleme girişimi yapıldı.(ID={request.Id})");
        return BaseResponseModel<CategoryDto>.Success(category,$"Tek kategori görüntüleme girişimi yapıldı.(ID={request.Id})");
    }
}