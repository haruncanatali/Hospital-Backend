using AutoMapper;
using Hospital.Application.Common.Helpers;
using Hospital.Application.Common.Mappings;
using Hospital.Domain.Identity;

namespace Hospital.Application.Users.Queries.GetUsersList
{
    public class UserDto : IMapFrom<User>
    {
        public long Id { get; set; }
        public string ProfilePhoto { get; set; }
        public string FirstAndLastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(dest => dest.FirstAndLastName,
                    opt => opt.MapFrom(x => string.Concat(x.FirstName, " ", x.LastName)))
                .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(x => x.Gender.GetDescription()));
        }
    }
}