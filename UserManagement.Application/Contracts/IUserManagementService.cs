using UserManagement.Domain.Enums;
using UserManagement.Application.DTOs;
using UserManagement.Application.Pagination;
using UserManagement.Application.DTOs.Requests;

namespace UserManagement.Application.Contracts;

public interface IUserManagementService
{
    Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto);
    Task<UserDto> GetUserAsync(Guid id);
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber, int pageSize);
    Task UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto);
    Task DeleteUserAsync(Guid id);
    Task AssignRoleAsync(Guid userId, UserRoleType roleType);
    Task RemoveRoleAsync(Guid userId, UserRoleType roleType);
    Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    Task ResetPasswordAsync(Guid userId, string newPassword);
}