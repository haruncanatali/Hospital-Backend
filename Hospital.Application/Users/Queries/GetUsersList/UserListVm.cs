namespace Hospital.Application.Users.Queries.GetUsersList;

public class UserListVm
{
    public IList<UserDto> Users { get; set; }

    public int Count { get; set; }
}