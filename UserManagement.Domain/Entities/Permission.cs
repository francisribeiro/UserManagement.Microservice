using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities;

public class Permission
{
    public Guid Id { get; protected set; }
    public PermissionType Type { get; protected set; }
    public bool Enabled { get; protected set; }
    public string Description { get; protected set; }
    
    protected Permission()
    {
    }

    public Permission(PermissionType type, string description, bool enabled = true)
    {
        Id = Guid.NewGuid();
        Type = type;
        Description = description;
        Enabled = enabled;
    }

    public void UpdateDescription(string description)
    {
        Description = description;
    }
    
    public void Enable()
    {
        Enabled = true;
    }

    public void Disable()
    {
        Enabled = false;
    }
}