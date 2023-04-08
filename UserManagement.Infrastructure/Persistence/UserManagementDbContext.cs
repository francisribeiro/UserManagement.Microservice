using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence;

public class UserManagementDbContext : DbContext
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<User?> Users { get; set; }
    public DbSet<Role?> Roles { get; set; }
    public DbSet<Permission?> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.Id);
            user.Property(u => u.FirstName).IsRequired();
            user.Property(u => u.LastName).IsRequired();
            user.Property(u => u.Email).IsRequired();
            user.Property(u => u.Password).IsRequired();
            user.HasMany(u => u.Roles).WithMany(r => r.Users);
        });

        modelBuilder.Entity<Role>(role =>
        {
            role.HasKey(r => r.Id);
            role.Property(r => r.Type).IsRequired();
            role.HasMany(r => r.Permissions).WithMany(p => p.Roles);
        });

        modelBuilder.Entity<Permission>(permission =>
        {
            permission.HasKey(p => p.Id);
            permission.Property(p => p.Type).IsRequired();
            permission.Property(p => p.Description).IsRequired();
        });

        // Configure many-to-many relationships
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));

        modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity(j => j.ToTable("RolePermissions"));
    }
}