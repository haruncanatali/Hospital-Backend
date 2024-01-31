using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Helpers;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Hospital.Application.Common.NotificationDispatcherServices;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Hospital.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Staves.Commands.Create;

public class CreateStaffCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Resume { get; set; }
    public Title Title { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    
    public class Handler : IRequestHandler<CreateStaffCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CreateStaffCommand> _logger;
        private readonly IHubStaffDispatcher _dispatcher;


        public Handler(IApplicationContext context, UserManager<User> userManager, ILogger<CreateStaffCommand> logger, IHubStaffDispatcher dispatcher)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _dispatcher = dispatcher;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (category == null)
            {
                _logger.LogCritical($"Personel için eklenecek kategori bulunamadı. ID:{request.CategoryId}");
                throw new BadRequestException($"Personel için eklenecek kategori bulunamadı. ID:{request.CategoryId}");
            }

            User? user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                _logger.LogCritical($"Personel için eklenecek kullanıcı bulunamadı. ID:{request.UserId}");
                throw new BadRequestException($"Personel için eklenecek kullanıcı bulunamadı. ID:{request.UserId}");
            }

            await _context.Staves.AddAsync(new Staff
            {
                Resume = request.Resume,
                Title = request.Title,
                CategoryId = request.CategoryId,
                UserId = request.UserId
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogCritical("Personel başarıyla eklendi.");
            var notificationMessage = $"{request.Title.GetDescription()} {user.FullName} adlı personel eklendi.";
            await _dispatcher.CrateStaffNotification(notificationMessage);
            return BaseResponseModel<Unit>.Success(Unit.Value,"Personel başarıyla eklendi.");
        }
    }
}