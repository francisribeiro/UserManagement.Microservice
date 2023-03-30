// using NUnit.Framework;
// using UserManagement.Domain.Exceptions;
//
// namespace UserManagement.Domain.Tests.Exceptions;
//
// public class UserNotFoundExceptionTests
// {
//     [Test]
//     public void Constructor_WithUserId_SetsMessage()
//     {
//         // Arrange
//         var userId = Guid.NewGuid();
//
//         // Act
//         var exception = new UserNotFoundException(userId);
//
//         // Assert
//         Assert.That(exception.Message, Is.EqualTo($"User with ID '{userId}' was not found."));
//     }
// }