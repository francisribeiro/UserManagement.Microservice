using AutoMapper;

namespace UserManagement.Infrastructure.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // CreateMap<User, UserDto>()
        //     .ForMember(
        //         dest => dest.Roles,
        //         opt => opt.MapFrom(src => src.Roles)
        //     );
        // CreateMap<UserCreateDto, User>();
        // CreateMap<UserUpdateDto, User>().ReverseMap();
    }
}