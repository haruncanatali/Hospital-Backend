using Microsoft.AspNetCore.Identity;

namespace Hospital.Domain.Identity;

public class Role : IdentityRole<long>
{
    public Role() : base()
    {

    }

    public Role(string roleName) : base(roleName)
    {

    }
}