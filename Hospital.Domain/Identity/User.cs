using System.Runtime.Serialization;
using Hospital.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Domain.Identity;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string ProfilePhoto { get; set; }
    public DateTime Birthdate { get; set; }
    public string IdentityNumber { get; set; }
    public string Address { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiredTime { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long? DeletedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    [IgnoreDataMember]
    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }
}