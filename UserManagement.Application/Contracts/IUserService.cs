using UserManagement.Domain.Enums;
using UserManagement.Application.DTOs;
using UserManagement.Application.Pagination;
using UserManagement.Application.DTOs.Requests;

namespace UserManagement.Application.Contracts
{
    public interface IUserService
    {
        // User management
        Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto);
        Task UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task DeleteUserByIdAsync(Guid id);

        // Password management
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task ResetPasswordAsync(Guid userId, string newPassword);

        // Role management
        Task AssignRoleAsync(Guid userId, UserRoleType roleType);
        Task RemoveRoleAsync(Guid userId, UserRoleType roleType);

        // User list and pagination
        Task<IEnumerable<UserDto>> GetUsersListAsync();
        Task<PagedResult<UserDto>> GetUsersListPagedAsync(int pageNumber, int pageSize);

        // Authentication
        Task<UserDto> LoginAsync(string email, string password);

        // Utility
        Task CheckEmailExistsAsync(string email);
    }
}