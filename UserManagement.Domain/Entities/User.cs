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
    public bool Enabled { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public DateTime? LastLogin { get; protected set; }
    public virtual ICollection<Role> Roles { get; protected set; }
    public List<IDomainEvent> DomainEvents { get; } = new();

    protected User()
    {
    }

    public User(string firstName, string lastName, string email, string password, bool enabled = true)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = new EmailAddress(email);
        Password = Password.CreateFromPlainText(password);
        Enabled = enabled;
        CreatedAt = DateTime.UtcNow;
        Roles = new HashSet<Role>();

        DomainEvents.Add(new UserCreatedEvent(this));
    }

    public void UpdateLoginDate()
    {
        LastLogin = DateTime.UtcNow;
        DomainEvents.Add(new UserLoggedInEvent(this));
    }

    private void UpdateUserDate()
    {
        UpdatedAt = DateTime.UtcNow;
        DomainEvents.Add(new UserUpdatedEvent(this));
    }

    public void UpdateUser(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = new EmailAddress(email);
        UpdateUserDate();
    }

    public void AssignRole(Role role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));
        if (HasRole(role.Type)) throw new UserRoleException("User already has this role.");

        Roles.Add(role);
        UpdateUserDate();
    }

    public void RemoveRole(Role role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));
        if (!HasRole(role.Type)) throw new UserRoleException("User does not have this role.");

        Roles.Remove(role);
        UpdateUserDate();
    }

    public bool HasRole(UserRoleType roleType) => Roles.Any(r => r.Type == roleType);

    public void ChangePassword(string currentPassword, string newPassword)
    {
        if (!Password.Verify(currentPassword)) throw new InvalidOperationException("Current password is incorrect.");
        if (Password.EqualTo(newPassword))
            throw new InvalidOperationException("New password cannot be equal to current password.");

        Password = Password.CreateFromPlainText(newPassword);
        DomainEvents.Add(new PasswordChangeEvent(this));
        UpdateUserDate();
    }

    public void ResetPassword(string newPassword)
    {
        Password = Password.CreateFromPlainText(newPassword);
        DomainEvents.Add(new PasswordResetEvent(this));
        UpdateUserDate();
    }

    public void Enable()
    {
        Enabled = true;
        UpdateUserDate();
    }

    public void Disable()
    {
        Enabled = false;
        UpdateUserDate();
    }
}