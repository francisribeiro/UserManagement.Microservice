using NUnit.Framework;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Specifications;

namespace UserManagement.Domain.Tests.Specifications;

public class UserHasRoleSpecificationTests
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

        var adminRole = new Role(UserRoleType.Administrator);
        _users[0].AssignRole(adminRole);
        _users[1].AssignRole(adminRole);
    }

    [Test]
    public void ToExpression_FilterUsersWithAdminRole_ReturnsUsersWithAdminRole()
    {
        // Arrange
        var specification = new UserHasRoleSpecification(UserRoleType.Administrator);

        // Act
        var usersWithAdminRole = _users.AsQueryable().Where(specification.ToExpression()).ToList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(usersWithAdminRole, Has.Count.EqualTo(2));
            Assert.That(usersWithAdminRole, Does.Contain(_users[0]));
            Assert.That(usersWithAdminRole, Does.Contain(_users[1]));
        });
    }
}