using UserManagement.Application.DTOs;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Contracts;

public interface IUserManagementService
{
    Task<UserDTO> CreateUserAsync(UserCreateDTO userCreateDTO);
    Task<UserDTO> GetUserAsync(Guid id);
    Task<IEnumerable<UserDTO>> GetUsersAsync();
    Task UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDTO);
    Task DeleteUserAsync(Guid id);
    Task AssignRoleAsync(Guid userId, UserRoleType roleType);
    Task RemoveRoleAsync(Guid userId, UserRoleType roleType);
    Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    Task ResetPasswordAsync(Guid userId, string newPassword);
}