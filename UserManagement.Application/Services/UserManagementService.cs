using AutoMapper;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Exceptions;
using UserManagement.Application.Contracts;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Validation;

namespace UserManagement.Application.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserManagementService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        DtoValidator.Validate(userCreateDto);

        var existingUser = await _userRepository.FindByEmailAsync(userCreateDto.Email);

        if (existingUser != null)
            throw new EmailAlreadyExistsException(userCreateDto.Email);

        var newUser = new User(
            userCreateDto.FirstName,
            userCreateDto.LastName,
            userCreateDto.Email,
            userCreateDto.Password);

        await _userRepository.CreateAsync(newUser);

        return _mapper.Map<UserDto>(newUser);
    }

    public async Task<UserDto> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return user == null ? throw new UserNotFoundException(id) : _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
    {
        DtoValidator.Validate(userUpdateDto);

        var user = await _userRepository.GetByIdAsync(id) ?? throw new UserNotFoundException(id);

        _mapper.Map(userUpdateDto, user);

        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new UserNotFoundException(id);

        await _userRepository.DeleteAsync(user);
    }

    public async Task AssignRoleAsync(Guid userId, UserRoleType roleType)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId);

        user.AssignRole(new Role(roleType));

        await _userRepository.UpdateAsync(user);
    }

    public async Task RemoveRoleAsync(Guid userId, UserRoleType roleType)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId);

        user.RemoveRole(new Role(roleType));

        await _userRepository.UpdateAsync(user);
    }

    public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId);

        if (!user.Password.Verify(currentPassword))
            throw new InvalidPasswordException();

        user.ChangePassword(currentPassword, newPassword);

        await _userRepository.UpdateAsync(user);
    }

    public async Task ResetPasswordAsync(Guid userId, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId);

        user.ResetPassword(newPassword);

        await _userRepository.UpdateAsync(user);
    }
}