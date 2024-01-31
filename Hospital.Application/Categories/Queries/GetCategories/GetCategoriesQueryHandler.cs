using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Categories.Queries.Dtos;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, BaseResponseModel<GetCategoriesVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCategoriesQueryHandler> _logger;

    public GetCategoriesQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetCategoriesQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<GetCategoriesVm>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        List<CategoryDto> categories = await _context.Categories
            .Where(c => (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())))
            .Include(c=>c.Staves)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        _logger.LogCritical("Çoklu kategori çekme girişimi yapıldı.");
        return BaseResponseModel<GetCategoriesVm>.Success(new GetCategoriesVm
        {
            Categories = categories,
            Count = categories.Count
        }, "Çoklu kategori çekme girişimi yapıldı.");
    }
}