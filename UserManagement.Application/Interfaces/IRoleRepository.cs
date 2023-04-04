using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Contracts;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync(Guid id);
    Task<IEnumerable<Role>> GetAllAsync();
    Task CreateAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(Role role);
    Task<bool> RoleExistsAsync(UserRoleType roleType);
}