using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Managers;
using MediatR;
using Hospital.Application.Common.Models;
using Hospital.Domain.Enums;
using Hospital.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace Hospital.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<BaseResponseModel<long>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Photo { get; set; }
        public string RoleName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }

        public class Handler : IRequestHandler<CreateUserCommand, BaseResponseModel<long>>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;
            private readonly IApplicationContext _context;
            private readonly FileManager _fileManager;
            private readonly ILogger<CreateUserCommand> _logger;

            public Handler(UserManager<User> userManager, RoleManager<Role> roleManager, FileManager fileManager,
                IApplicationContext context, ILogger<CreateUserCommand> logger)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _fileManager = fileManager;
                _context = context;
                _logger = logger;
            }

            public async Task<BaseResponseModel<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    bool dublicateControl = _context.Users.Any(x => x.UserName == request.IdentityNumber || x.Email == request.Email);
                    if (dublicateControl)
                    {
                        _logger.LogCritical("Eklenmek istenen kullanıcı mükerrer kontrolden geçemedi. Aynı kullanıcı adı veya E-Posta'ya sahip kullanıcı sistemde mevcut görünüyor.");
                        throw new BadRequestException("Eklenmek istenen kullanıcı mükerrer kontrolden geçemedi. Aynı kullanıcı adı veya E-Posta'ya sahip kullanıcı sistemde mevcut görünüyor.");
                    }

                    string profilePhoto = String.Empty;
                    if (request.Photo != null)
                    {
                        profilePhoto = _fileManager.Upload(request.Photo, FileRoot.UserProfile);
                    }

                    User entity = new()
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        UserName = request.Email,
                        Email = request.Email,
                        Gender = request.Gender,
                        ProfilePhoto = profilePhoto,
                        Address = request.Address,
                        IdentityNumber = request.IdentityNumber,
                        Birthdate = request.Birthdate,
                        RefreshToken = String.Empty,
                        RefreshTokenExpiredTime = DateTime.Now,
                        PhoneNumber = request.Phone,
                        EmailConfirmed = request.EmailConfirmed,
                        PhoneNumberConfirmed = request.PhoneConfirmed
                    };

                    var response = await _userManager.CreateAsync(entity, request.Password);

                    Role? role = await _roleManager.FindByNameAsync(request.RoleName);
                    
                    if (role != null)
                    {
                        _logger.LogCritical($"Kullanıcı oluşturulurken başarıyla role eklendi. RoleId:{role.Id} Username:{entity.UserName}");
                        await _userManager.AddToRoleAsync(entity, request.RoleName);
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
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Kullanıcı ({request.Email}) eklenirken hata meydana geldi. Hata: {e.Message}");
                    throw new BadRequestException($"Kullanıcı ({request.Email}) eklenirken hata meydana geldi. Hata: {e.Message}");
                }

                _logger.LogCritical($"Kullanıcı başarıyla eklendi. Username:{request.Email}");
                return BaseResponseModel<long>.Success(1, $"Kullanıcı başarıyla eklendi. Username:{request.Email}");
            }
        }
    }
}