using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Application.Patients.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Patients.Queries.GetPatient;

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, BaseResponseModel<PatientDto>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPatientQueryHandler> _logger;

    public GetPatientQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetPatientQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<PatientDto>> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        PatientDto? patient = await _context.Patients
            .Where(c => c.Id == request.Id)
            .Include(c => c.User)
            .Include(c => c.Appointments)
            .ProjectTo<PatientDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        _logger.LogCritical($"Tekil hasta görüntüleme girişimi yapıldı. ID:{request.Id}");
        return BaseResponseModel<PatientDto>.Success(patient,$"Tekil hasta görüntüleme girişimi yapıldı. ID:{request.Id}");
    }
}