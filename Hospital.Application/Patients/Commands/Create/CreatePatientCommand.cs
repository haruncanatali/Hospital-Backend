using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Domain.Entities;
using Hospital.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Patients.Commands.Create;

public class CreatePatientCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Nationality { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
    public long UserId { get; set; }
    
    public class Handler : IRequestHandler<CreatePatientCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CreatePatientCommand> _logger;

        public Handler(IApplicationContext context, UserManager<User> userManager, ILogger<CreatePatientCommand> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                _logger.LogCritical($"Hasta bulunamadı. ID:{request.UserId}");
                throw new BadRequestException($"Hasta bulunamadı. ID:{request.UserId}");
            }

            await _context.Patients.AddAsync(new Patient
            {   
                Nationality = request.Nationality,
                Address = request.Address,
                Notes = request.Notes,
                UserId = request.UserId
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogCritical("Hasta başarıyla eklendi.");
            return BaseResponseModel<Unit>.Success(Unit.Value, "Hasta başarıyla eklendi.");
        }
    }
}