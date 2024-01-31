using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Application.Staves.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Staves.Queries.GetStaves;

public class GetStavesQueryHandler : IRequestHandler<GetStavesQuery, BaseResponseModel<GetStavesVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetStavesQueryHandler> _logger;

    public GetStavesQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetStavesQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<GetStavesVm>> Handle(GetStavesQuery request, CancellationToken cancellationToken)
    {
        List<StaffDto> staves = await _context.Staves
            .Include(c => c.User)
            .Include(c => c.Category)
            .Include(c => c.Appointments)
            .Where(c => (request.IdentityNumber == null || c.User.IdentityNumber.Contains(request.IdentityNumber)))
            .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        _logger.LogCritical("Çoklu personel çekme girişimi yapıldı.");
        return BaseResponseModel<GetStavesVm>.Success(new GetStavesVm
        {
            Staves = staves,
            Count = staves.Count
        }, "Success");
    }
}