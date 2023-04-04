using UserManagement.Application.DTOs;

namespace UserManagement.Application.Contracts;

public interface IPermissionService
{
    Task<PermissionDto> CreatePermissionAsync(PermissionDto permissionDto);
    Task UpdatePermissionAsync(Guid id, PermissionDto permissionDto);
    Task DeletePermissionAsync(Guid id);
    Task<PermissionDto> GetPermissionByIdAsync(Guid id);
    Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
}
