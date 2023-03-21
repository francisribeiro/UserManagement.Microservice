using NUnit.Framework;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Specifications;

namespace UserManagement.Domain.Tests.Specifications
{
    public class UserWithEmailSpecificationTests
    {
        private List<User> _users;

        public UserWithEmailSpecificationTests(List<User> users)
        {
            _users = users;
        }

        [SetUp]
        public void SetUp()
        {
            _users = new List<User>
            {
                new User("John", "Doe", "john.doe@example.com", "Password123!"),
                new User("Jane", "Doe", "jane.doe@example.com", "Password123!"),
                new User("James", "Doe", "james.doe@example.com", "Password123!")
            };
        }

        [Test]
        public void ToExpression_FilterUsersWithEmailContainingDoe_ReturnsAllUsers()
        {
            // Arrange
            var specification = new UserWithEmailSpecification("doe");

            // Act
            var filteredUsers = _users.AsQueryable().Where(specification.ToExpression()).ToList();

            // Assert
            Assert.That(filteredUsers, Has.Count.EqualTo(3));
        }

        [Test]
        public void ToExpression_FilterUsersWithEmailContainingJane_ReturnsOneUser()
        {
            // Arrange
            var specification = new UserWithEmailSpecification("jane");

            // Act
            var filteredUsers = _users.AsQueryable().Where(specification.ToExpression()).ToList();

            // Assert
            Assert.That(filteredUsers, Has.Count.EqualTo(1));
            Assert.That(filteredUsers.Single(), Is.EqualTo(_users[1]));
        }
    }
}