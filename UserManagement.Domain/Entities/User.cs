using UserManagement.Domain.Enums;
using UserManagement.Domain.Events;
using UserManagement.Domain.Exceptions;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.Entities;

public class User
{
    public Guid Id { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public EmailAddress Email { get; protected set; }
    public Password Password { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? LastLogin { get; protected set; }
    public virtual ICollection<UserRole> UserRoles { get; protected set; }
    public List<IDomainEvent> DomainEvents { get; } = new List<IDomainEvent>();

    protected User()
    {
    }

    public User(string firstName, string lastName, string email, string password)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = new EmailAddress(email);
        Password = Password.CreateFromPlainText(password);
        CreatedAt = DateTime.UtcNow;
        UserRoles = new List<UserRole>();

        DomainEvents.Add(new UserCreatedEvent(this));
    }

    public void UpdateLoginDate()
    {
        LastLogin = DateTime.UtcNow;
        DomainEvents.Add(new UserLoggedInEvent(this));
    }

    public void AssignRole(Role role)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        if (HasRole(role.Type))
            throw new UserRoleException("User already has this role.");

        UserRoles.Add(new UserRole(this, role));
    }

    public void RemoveRole(Role role)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        var userRole = UserRoles.SingleOrDefault(ur => ur.RoleId == role.Id);
        if (userRole == null)
            throw new UserRoleException("User does not have this role.");

        UserRoles.Remove(userRole);
    }

    public bool HasRole(UserRoleType roleType)
    {
        return UserRoles.Any(ur => ur.Role.Type == roleType);
    }

    public void ChangePassword(string currentPassword, string newPassword)
    {
        if (!Password.Verify(currentPassword))
            throw new InvalidOperationException("Current password is incorrect.");

        Password = Password.CreateFromPlainText(newPassword);
    }

    public void ResetPassword(string newPassword)
    {
        Password = Password.CreateFromPlainText(newPassword);
        DomainEvents.Add(new PasswordResetEvent(this));
    }
}