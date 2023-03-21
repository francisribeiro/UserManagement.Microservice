namespace UserManagement.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; protected set; }
    public Guid RoleId { get; protected set; }
    public virtual User User { get; protected set; }
    public virtual Role Role { get; protected set; }

    protected UserRole()
    {
    }

    public UserRole(User user, Role role)
    {
        User = user;
        Role = role;
        UserId = user.Id;
        RoleId = role.Id;
    }
}