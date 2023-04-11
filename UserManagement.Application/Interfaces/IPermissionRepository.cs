using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(PermissionType type);
    Task<IEnumerable<Permission?>> GetAllAsync();
    Task<Permission?> CreateAsync(Permission permission);
    Task UpdateAsync(Permission permission);
    Task DeleteAsync(Permission permission);
    Task EnsurePermissionNotInUseAsync(Guid id);
}