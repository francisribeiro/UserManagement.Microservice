using NUnit.Framework;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Specifications;

namespace UserManagement.Domain.Tests.Specifications;

public class UserLoggedInWithinDateRangeSpecificationTests
{
    private List<User> _users;

    [SetUp]
    public void SetUp()
    {
        _users = new List<User>
        {
            new User("John", "Doe", "john.doe@example.com", "Password123!"),
            new User("Jane", "Doe", "jane.doe@example.com", "Password123!"),
            new User("James", "Doe", "james.doe@example.com", "Password123!")
        };

        _users[0].UpdateLoginDate(); // Sets LastLogin to DateTime.UtcNow
        _users[1].UpdateLoginDate(); // Sets LastLogin to DateTime.UtcNow
        _users[2].UpdateLoginDate(); // Sets LastLogin to DateTime.UtcNow
    }

    [Test]
    public void ToExpression_FilterUsersLoggedInToday_ReturnsAllUsers()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var specification = new UserLoggedInWithinDateRangeSpecification(today, today.AddDays(1).AddTicks(-1));

        // Act
        var filteredUsers = _users.AsQueryable().Where(specification.ToExpression()).ToList();

        // Assert
        Assert.That(filteredUsers, Has.Count.EqualTo(3));
    }

    [Test]
    public void ToExpression_FilterUsersLoggedInYesterday_ReturnsNoUsers()
    {
        // Arrange
        var yesterday = DateTime.UtcNow.Date.AddDays(-1);
        var specification = new UserLoggedInWithinDateRangeSpecification(yesterday, yesterday.AddDays(1).AddTicks(-1));

        // Act
        var filteredUsers = _users.AsQueryable().Where(specification.ToExpression()).ToList();

        // Assert
        Assert.That(filteredUsers, Is.Empty);
    }
}