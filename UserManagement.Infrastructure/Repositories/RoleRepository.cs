using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Exceptions.Role;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly UserManagementDbContext _dbContext;

    public RoleRepository(UserManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Roles.FindAsync(id);
    }

    public async Task<IEnumerable<Role?>> GetAllAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public async Task CreateAsync(Role? role)
    {
        await _dbContext.Roles.AddAsync(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role? role)
    {
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Role? role)
    {
        _dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> RoleExistsAsync(UserRoleType type)
    {
        return await _dbContext.Roles.AnyAsync(r => r.Type == type);
    }

    public async Task EnsureRoleNotInUseAsync(Guid id)
    {
        var role = await _dbContext.Roles.FindAsync(id);
        if (role == null)
            throw new RoleNotFoundException(id);

        var roleInUse = await _dbContext.Users
            .AnyAsync(u => u.Roles.Any(r => r.Id == id));

        if (roleInUse)
            throw new RoleInUseException(role.Type);
    }
}