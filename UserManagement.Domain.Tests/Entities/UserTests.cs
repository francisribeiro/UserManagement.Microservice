using NUnit.Framework;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Events;
using UserManagement.Domain.Exceptions;

namespace UserManagement.Domain.Tests.Entities;

public class UserTests
{
    [Test]
    public void Create_WithValidData_CreatesUser()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "test@example.com";
        const string password = "Password123!";

        // Act
        var user = new User(firstName, lastName, email, password);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.FirstName, Is.EqualTo(firstName));
            Assert.That(user.LastName, Is.EqualTo(lastName));
            Assert.That(user.Email.Value, Is.EqualTo(email));
            Assert.That(user.Password.Verify(password), Is.True);
        });
    }

    [Test]
    public void UpdateLoginDate_SetsLastLoginAndRaisesUserLoggedInEvent()
    {
        // Arrange
        var user = CreateUser();

        // Act
        user.UpdateLoginDate();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.LastLogin, Is.Not.Null);
            Assert.That(user.DomainEvents.Any(e => e.GetType() == typeof(UserLoggedInEvent)), Is.True);
        });
    }

    [Test]
    public void AddRole_AddsRoleToUserRoles()
    {
        // Arrange
        var user = CreateUser();
        var role = new Role(UserRoleType.Administrator);

        // Act
        user.AddRole(role);

        // Assert
        Assert.That(user.HasRole(UserRoleType.Administrator), Is.True);
    }

    [Test]
    public void RemoveRole_RemovesRoleFromUserRoles()
    {
        // Arrange
        var user = CreateUser();
        var role = new Role(UserRoleType.Administrator);
        user.AddRole(role);

        // Act
        user.RemoveRole(role);

        // Assert
        Assert.That(user.HasRole(UserRoleType.Administrator), Is.False);
    }

    [Test]
    public void ChangePassword_WithValidCurrentPassword_ChangesPassword()
    {
        // Arrange
        var user = CreateUser();
        const string currentPassword = "Password123!";
        const string newPassword = "NewPassword123!";

        // Act
        user.ChangePassword(currentPassword, newPassword);

        // Assert
        Assert.That(user.Password.Verify(newPassword), Is.True);
    }

    [Test]
    public void ChangePassword_WithInvalidCurrentPassword_ThrowsInvalidOperationException()
    {
        // Arrange
        var user = CreateUser();
        const string currentPassword = "WrongPassword123!";
        const string newPassword = "NewPassword123!";

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => user.ChangePassword(currentPassword, newPassword));
    }

    [Test]
    public void ResetPassword_WithValidNewPassword_ResetsPassword()
    {
        // Arrange
        var user = CreateUser();
        const string newPassword = "NewPassword123!";

        // Act
        user.ResetPassword(newPassword);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.Password.Verify(newPassword), Is.True);
            Assert.That(user.DomainEvents.Any(e => e.GetType() == typeof(PasswordResetEvent)), Is.True);
        });
    }

    [Test]
    public void AddRole_AddingExistingRole_ThrowsUserRoleException()
    {
        // Arrange
        var user = CreateUser();
        var role = new Role(UserRoleType.Administrator);
        user.AddRole(role);

        // Act & Assert
        Assert.Throws<UserRoleException>(() => user.AddRole(role));
    }

    [Test]
    public void RemoveRole_RemovingNonExistentRole_ThrowsUserRoleException()
    {
        // Arrange
        var user = CreateUser();
        var role = new Role(UserRoleType.Administrator);

        // Act & Assert
        Assert.Throws<UserRoleException>(() => user.RemoveRole(role));
    }

    [Test]
    public void HasRole_WhenUserHasRole_ReturnsTrue()
    {
        // Arrange
        var user = CreateUser();
        var role = new Role(UserRoleType.Administrator);
        user.AddRole(role);

        // Act
        var hasRole = user.HasRole(UserRoleType.Administrator);

        // Assert
        Assert.That(hasRole, Is.True);
    }

    [Test]
    public void HasRole_WhenUserDoesNotHaveRole_ReturnsFalse()
    {
        // Arrange
        var user = CreateUser();

        // Act
        var hasRole = user.HasRole(UserRoleType.Administrator);

        // Assert
        Assert.That(hasRole, Is.False);
    }


    [Test]
    public void Constructor_WithNullOrEmptyEmail_ThrowsArgumentException()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        string email = null;
        const string password = "Password123!";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var user = new User(firstName, lastName, email, password);
        });
    }

    [Test]
    public void Constructor_WithNullOrEmptyPassword_ThrowsArgumentException()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "test@example.com";
        string password = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var user = new User(firstName, lastName, email, password);
        });
    }

    [Test]
    public void AddRole_WithNullRole_ThrowsArgumentNullException()
    {
        // Arrange
        var user = CreateUser();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => user.AddRole(null));
    }

    [Test]
    public void RemoveRole_WithNullRole_ThrowsArgumentNullException()
    {
        // Arrange
        var user = CreateUser();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => user.RemoveRole(null));
    }

    [Test]
    public void UpdateLoginDate_UpdatesLastLoginToRecentTime()
    {
        // Arrange
        var user = CreateUser();
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        user.UpdateLoginDate();
        var afterUpdate = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.LastLogin, Is.GreaterThanOrEqualTo(beforeUpdate));
            Assert.That(user.LastLogin, Is.LessThanOrEqualTo(afterUpdate));
        });
    }

    private static User CreateUser()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "test@example.com";
        const string password = "Password123!";

        return new User(firstName, lastName, email, password);
    }
}