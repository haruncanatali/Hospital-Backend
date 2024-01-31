using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Managers;
using Hospital.Application.Common.Models;
using Hospital.Domain.Enums;
using Hospital.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<BaseResponseModel<long>>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, BaseResponseModel<long>>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;
            private readonly IApplicationContext _context;
            private readonly FileManager _fileManager;
            private readonly ILogger<UpdateUserCommand> _logger;

            public Handler(IApplicationContext context, FileManager fileManager, UserManager<User> userManager, ILogger<UpdateUserCommand> logger, RoleManager<Role> roleManager)
            {
                _context = context;
                _fileManager = fileManager;
                _userManager = userManager;
                _logger = logger;
                _roleManager = roleManager;
            }

            public async Task<BaseResponseModel<long>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    User? entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
                    string profilePhoto = entity.ProfilePhoto;
                    if (request.ProfilePhoto != null)
                    {
                        profilePhoto = _fileManager.Upload(request.ProfilePhoto, FileRoot.UserProfile);
                    }

                    entity.FirstName = request.FirstName;
                    entity.LastName = request.LastName;
                    entity.Email = request.Email;
                    entity.Gender = request.Gender;
                    entity.ProfilePhoto = profilePhoto;
                    entity.IdentityNumber = request.IdentityNumber;
                    entity.Address = request.Address;
                    entity.PhoneNumber = request.Phone;
                    entity.EmailConfirmed = request.EmailConfirmed;
                    entity.PhoneNumberConfirmed = request.PhoneConfirmed;
                    entity.Birthdate = request.Birthdate;
                    
                    await _userManager.UpdateAsync(entity);

                    if (!string.IsNullOrEmpty(request.Password))
                    {
                        var removeResult = await _userManager.RemovePasswordAsync(entity);
                        if (removeResult.Succeeded)
                        {
                            await _userManager.AddPasswordAsync(entity, request.Password);
                        }
                        else
                        {
                            _logger.LogCritical($"(UUC-0) Kullanıcı güncellenirken şifre değiştirme sırasında hata meydana geldi.");
                            throw new BadRequestException(
                                $"(UUC-0) Kullanıcı güncellenirken şifre değiştirme sırasında hata meydana geldi.");
                        }
                    }

                    UserRole? userRole =
                        await _context.UserRoles.Where(x => x.UserId == entity.Id).FirstOrDefaultAsync();
                    Role? role = await _context.Roles.Where(x => x.Name == request.RoleName).FirstOrDefaultAsync();
                    if (userRole.RoleId != role.Id)
                    {
                        userRole.RoleId = role.Id;
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        Role? normalRole = await _roleManager.FindByNameAsync("Normal");
                        if (normalRole == null)
                        {
                            await _roleManager.CreateAsync(new Role
                            {
                                Name = "Normal"
                            });
                        }

                        await _userManager.AddToRoleAsync(entity, "Normal");
                        _logger.LogCritical($"Kullanıcı oluşturulurken eklenmek istenen {request.RoleName} rolü bulunamadı. Normal role eklendi.");
                    }
                    
                    _logger.LogCritical($"Kullanıcı başarıyla oluşturuldu. Username: {request.Email}");
                    return BaseResponseModel<long>.Success(entity.Id);
                }
                catch (Exception e)
                {
                    throw new BadRequestException(
                        $"({request.Email}) Kullanıcı oluşturulurken hata meydana geldi. Hata: {e.Message}");
                }
            }
        }
    }
}