using MediatR;

namespace Hospital.Application.Users.Queries.GetUsersList
{
    public class GetUserListQuery : IRequest<UserListVm>
    {
        public string? IdentityNumber { get; set; }
    }
}
