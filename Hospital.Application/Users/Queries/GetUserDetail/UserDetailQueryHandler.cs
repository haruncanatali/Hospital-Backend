using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.Application.Common.Exceptions;
using Hospital.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Users.Queries.GetUserDetail
{
    public class UserDetailQueryHandler : IRequestHandler<UserDetailQuery, UserDetailDto>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserDetailQueryHandler> _logger;

        public UserDetailQueryHandler(IApplicationContext context, IMapper mapper, ILogger<UserDetailQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDetailDto> Handle(UserDetailQuery request, CancellationToken cancellationToken)
        {
            UserDetailDto? user = await _context.Users
                .ProjectTo<UserDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (user == null)
            {
                _logger.LogCritical($"(TKGG-0) Kullanıcı bulunamadı. ID:{request.Id}");
                throw new BadRequestException($"(TKGG-0) Kullanıcı bulunamadı. ID:{request.Id}");
            }
            
            long roleId = await _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId)
                .FirstOrDefaultAsync(cancellationToken);
            user.Roles = await _context.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync(cancellationToken);
            _logger.LogCritical($"Tekil kullanıcı görüntüleme girişiminde bulunuldu. UserID:{request.Id}");
            return user;
        }
    }
}