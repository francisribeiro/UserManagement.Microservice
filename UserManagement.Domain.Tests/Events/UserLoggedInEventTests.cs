// using NUnit.Framework;
// using UserManagement.Domain.Entities;
// using UserManagement.Domain.Events;
//
// namespace UserManagement.Domain.Tests.Events;
//
// public class UserLoggedInEventTests
// {
//     private User CreateUser()
//     {
//         // Create a User instance with sample data
//         return new User("John", "Doe", "john.doe@example.com", "Password123!");
//     }
//
//     [Test]
//     public void Constructor_WithUserParameter_SetsUser()
//     {
//         // Arrange
//         var user = CreateUser();
//
//         // Act
//         var userLoggedInEvent = new UserLoggedInEvent(user);
//
//         // Assert
//         Assert.That(userLoggedInEvent.User, Is.EqualTo(user));
//     }
// }