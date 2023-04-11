using UserManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Application.Interfaces;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Application.Exceptions.Permission;

namespace UserManagement.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly UserManagementDbContext _context;

    public PermissionRepository(UserManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        return await _context.Permissions.FindAsync(id);
    }

    public async Task<bool> ExistsAsync(PermissionType type)
    {
        return await _context.Permissions.AnyAsync(p => p.Type == type);
    }

    public async Task<IEnumerable<Permission?>> GetAllAsync()
    {
        return await _context.Permissions.ToListAsync();
    }

    public async Task<Permission?> CreateAsync(Permission permission)
    {
        var result = await _context.Permissions.AddAsync(permission);
        await _context.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task UpdateAsync(Permission permission)
    {
        _context.Entry(permission).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Permission permission)
    {
        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync();
    }

    public async Task EnsurePermissionNotInUseAsync(Guid id)
    {
        var isPermissionInUse = await _context.Roles.AnyAsync(r => r.Permissions.Any(p => p.Id == id));

        if (isPermissionInUse)
            throw new PermissionInUseException(id);
    }
}