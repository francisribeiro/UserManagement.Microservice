using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence;

public class UserManagementDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.Id);
            user.OwnsOne(u => u.Email);
            user.HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity(join => join.ToTable("UserRoles"));
        });

        modelBuilder.Entity<Role>(role =>
        {
            role.HasKey(r => r.Id);
            role.HasMany(r => r.Permissions)
                .WithMany()
                .UsingEntity(join => join.ToTable("RolePermissions"));
        });

        modelBuilder.Entity<Permission>(permission =>
        {
            permission.HasKey(p => p.Id);
        });
    }
}
