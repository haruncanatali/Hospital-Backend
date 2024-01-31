using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Application.Patients.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Patients.Queries.GetPatients;

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, BaseResponseModel<GetPatientsVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPatientsQueryHandler> _logger;

    public GetPatientsQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetPatientsQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<GetPatientsVm>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        List<PatientDto> patients = await _context.Patients
            .Include(c => c.User)
            .Include(c => c.Appointments)
            .Where(c => (request.IdentityNumber == null || c.User.IdentityNumber.Contains(request.IdentityNumber)))
            .ProjectTo<PatientDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        _logger.LogCritical("Çoklu hasta çekme girişimi yapıldı.");
        return BaseResponseModel<GetPatientsVm>.Success(new GetPatientsVm
        {
            Patients = patients,
            Count = patients.Count
        }, "Success");
    }
}