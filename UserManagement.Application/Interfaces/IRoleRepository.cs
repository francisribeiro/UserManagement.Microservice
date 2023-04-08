using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<IEnumerable<Role?>> GetAllAsync();
    Task CreateAsync(Role? role);
    Task UpdateAsync(Role? role);
    Task DeleteAsync(Role? role);
    Task<bool> RoleExistsAsync(UserRoleType type);
    Task EnsureRoleNotInUseAsync(Guid id);
}