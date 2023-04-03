using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.DTOs;

/// <summary>
/// Data transfer object for the Permission entity.
/// </summary>
public class PermissionDto
{
    public Guid Id { get; set; }
    public PermissionType Type { get; set; }
    public bool Enabled { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the PermissionDto class using the given Permission entity.
    /// </summary>
    /// <param name="permission">The Permission entity to be converted into a PermissionDto.</param>
    public PermissionDto(Permission permission)
    {
        Id = permission.Id;
        Type = permission.Type;
        Enabled = permission.Enabled;
        Description = permission.Description;
    }
}