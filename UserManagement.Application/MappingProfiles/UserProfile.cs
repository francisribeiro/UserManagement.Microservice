using AutoMapper;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(
                dest => dest.Roles,
                opt => opt.MapFrom(
                    src => src.UserRoles.Select(ur => ur.Role)
                )
            );
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>().ReverseMap();
    }
}