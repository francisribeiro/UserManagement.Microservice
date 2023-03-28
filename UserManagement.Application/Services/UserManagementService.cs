using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Exceptions;
using UserManagement.Application.Contracts;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IUserRepository _userRepository;

    public UserManagementService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        var existingUser = await _userRepository.FindByEmailAsync(userCreateDto.Email);

        if (existingUser != null)
            throw new EmailAlreadyExistsException(userCreateDto.Email);

        var newUser = new User(
            userCreateDto.FirstName,
            userCreateDto.LastName,
            userCreateDto.Email,
            userCreateDto.Password);

        await _userRepository.CreateAsync(newUser);

        return new UserDto(newUser);
    }

    public async Task<UserDto> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            throw new UserNotFoundException(id);

        return new UserDto(user);
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(user => new UserDto(user)).ToList();
    }

    public async Task UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            throw new UserNotFoundException(id);

        user.FirstName = userUpdateDto.FirstName;
        user.LastName = userUpdateDto.LastName;
        user.Email = userUpdateDto.Email;

        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            throw new UserNotFoundException(id);

        await _userRepository.DeleteAsync(user);
    }

    public async Task AssignRoleAsync(Guid userId, UserRoleType roleType)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new UserNotFoundException(userId);

        user.AssignRole(new Role(roleType));
        await _userRepository.UpdateAsync(user);
    }

    public async Task RemoveRoleAsync(Guid userId, UserRoleType roleType)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new UserNotFoundException(userId);
        
        user.RemoveRole(new Role(roleType));
        await _userRepository.UpdateAsync(user);
    }

    public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user == null)
            throw new UserNotFoundException(userId);

        if (!user.Password.Verify(currentPassword))
            throw new InvalidPasswordException();

        user.ChangePassword(currentPassword, newPassword);
        await _userRepository.UpdateAsync(user);
    }

    public async Task ResetPasswordAsync(Guid userId, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user == null)
            throw new UserNotFoundException(userId);
        
        user.ResetPassword(newPassword);
        await _userRepository.UpdateAsync(user);
    }
}