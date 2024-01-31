using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Models;
using Hospital.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Auth.Queries.HardPasswordChange;

public class HardPasswordChangeCommand : IRequest<BaseResponseModel<User>>
{
    public string Password { get; set; }
    public long UserId { get; set; }

    public class Handler : IRequestHandler<HardPasswordChangeCommand, BaseResponseModel<User>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HardPasswordChangeCommand> _logger;

        public Handler(UserManager<User> userManager, ILogger<HardPasswordChangeCommand> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<BaseResponseModel<User>> Handle(HardPasswordChangeCommand request, CancellationToken cancellationToken)
        {
            User? appUser = _userManager.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (appUser != null)
            {
                var removeResult = await _userManager.RemovePasswordAsync(appUser);
                if (removeResult.Succeeded)
                {
                    var addResult = await _userManager.AddPasswordAsync(appUser, request.Password);
                    if (addResult.Succeeded)
                    {
                        _logger.LogCritical($"Şifre değiştirildi. Güncelleme yapılan kullanıcı : {appUser.UserName}");
                        return BaseResponseModel<User>.Success(appUser,$"Şifre değiştirildi. Güncelleme yapılan kullanıcı : {appUser.UserName}");
                    }
                    else
                    {
                        _logger.LogCritical($"(HPCC-0) Şifre değiştirilemedi. Hata meydana geldi. Güncelleme yapılamıyan kullanıcı : {appUser.UserName}");
                        throw new BadRequestException($"Şifre değiştirilemedi. Hata meydana geldi. Güncelleme yapılamıyan kullanıcı : {appUser.UserName}");
                    }
                }
                else
                {
                    _logger.LogCritical($"(HPCC-1) Şifre değiştirilemedi. Hata meydana geldi. Güncelleme yapılamıyan kullanıcı : {appUser.UserName}");
                    throw new BadRequestException("Şifre Silinemedi!");
                }
            }
            
            _logger.LogCritical($"(HPCC-2) Şifre değiştirilemedi. Kullanıcı bulunamadı. Güncellenmek istenen kullanıcı ID:{request.UserId}");
            throw new NotFoundException(nameof(User), $"(HPCC-2) Şifre değiştirilemedi. Kullanıcı bulunamadı. Güncellenmek istenen kullanıcı ID:{request.UserId}");
        }
    }
}