using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Application.Staves.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Staves.Queries.GetStaff;

public class GetStaffQueryHandler : IRequestHandler<GetStaffQuery, BaseResponseModel<StaffDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetStaffQueryHandler> _logger;

    public GetStaffQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetStaffQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<StaffDto>> Handle(GetStaffQuery request, CancellationToken cancellationToken)
    {
        StaffDto? staff = await _context.Staves
            .Where(c => c.Id == request.Id)
            .Include(c => c.Category)
            .Include(c => c.User)
            .Include(c=>c.Appointments)
            .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        _logger.LogCritical($"Tek personel görüntüleme girişimi yapıldı.(ID={request.Id})");
        return BaseResponseModel<StaffDto>.Success(staff, $"Tek personel görüntüleme girişimi yapıldı.(ID={request.Id})");
    }
}