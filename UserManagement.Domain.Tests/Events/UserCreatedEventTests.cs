using NUnit.Framework;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Events;

namespace UserManagement.Domain.Tests.Events;

public class UserCreatedEventTests
{
    private User CreateUser()
    {
        // Create a User instance with sample data
        return new User("John", "Doe", "john.doe@example.com", "Password123!");
    }

    [Test]
    public void Constructor_WithUserParameter_SetsUser()
    {
        // Arrange
        var user = CreateUser();

        // Act
        var userCreatedEvent = new UserCreatedEvent(user);

        // Assert
        Assert.That(userCreatedEvent.User, Is.EqualTo(user));
    }
}