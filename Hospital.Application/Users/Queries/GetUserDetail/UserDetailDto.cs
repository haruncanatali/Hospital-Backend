using AutoMapper;
using Hospital.Application.Common.Mappings;
using Hospital.Domain.Enums;
using Hospital.Domain.Identity;

namespace Hospital.Application.Users.Queries.GetUserDetail
{
    public class UserDetailDto : IMapFrom<User>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePhoto { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailDto>();
        }
    }
}