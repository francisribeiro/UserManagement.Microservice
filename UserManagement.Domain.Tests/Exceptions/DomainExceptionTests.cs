// using NUnit.Framework;
// using UserManagement.Domain.Exceptions;
//
// namespace UserManagement.Domain.Tests.Exceptions;
//
// public class DomainExceptionTests
// {
//     [Test]
//     public void Constructor_WithMessage_SetsMessage()
//     {
//         // Arrange
//         const string message = "Domain exception occurred.";
//
//         // Act
//         var exception = new DomainException(message);
//
//         // Assert
//         Assert.That(exception.Message, Is.EqualTo(message));
//     }
//
//     [Test]
//     public void Constructor_WithMessageAndInnerException_SetsMessageAndInnerException()
//     {
//         // Arrange
//         const string message = "Domain exception occurred.";
//         var innerException = new Exception("Inner exception message.");
//
//         // Act
//         var exception = new DomainException(message, innerException);
//         
//         // Assert
//         Assert.Multiple(() =>
//         {
//             Assert.That(exception.Message, Is.EqualTo(message));
//             Assert.That(exception.InnerException, Is.EqualTo(innerException));
//         });
//     }
// }