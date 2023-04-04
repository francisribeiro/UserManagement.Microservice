using UserManagement.Domain.Enums;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Contracts;

public interface IRoleService
{
    Task<RoleDto> CreateRoleAsync(UserRoleType type);
    Task UpdateRoleAsync(Guid id, UserRoleType newRoleType);
    Task DeleteRoleAsync(Guid id);
    Task<RoleDto> GetRoleByIdAsync(Guid id);
    Task<IEnumerable<RoleDto>> GetRolesListAsync();
}
