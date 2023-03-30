using NUnit.Framework;
using UserManagement.Application.DTOs;
using UserManagement.Application.Services;
using UserManagement.Application.Contracts;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.Tests;

public class ValidationServiceTests
{
    private IValidationService _validationService;

    [SetUp]
    public void SetUp()
    {
        _validationService = new ValidationService();
    }

    [Test]
    public void Validate_WhenUserCreateDtoIsValid_DoesNotThrowException()
    {
        // Arrange
        var userCreateDto = new UserCreateDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "password123"
        };

        // Act & Assert
        Assert.DoesNotThrow(() => _validationService.Validate(userCreateDto));
    }

    [TestCase("John", "Doe", "invalid_email", "password123", TestName = "Validate_WhenEmailIsInvalid_ThrowsValidationException")]
    [TestCase("John", "Doe", "john.doe@example.com", "short", TestName = "Validate_WhenPasswordIsInvalid_ThrowsValidationException")]
    [TestCase("John", "Doe", "", "password123", TestName = "Validate_WhenEmailIsEmpty_ThrowsValidationException")]
    [TestCase("John", "Doe", "john.doe@example.com", "", TestName = "Validate_WhenPasswordIsEmpty_ThrowsValidationException")]
    public void Validate_WhenUserCreateDtoIsInvalid_ThrowsValidationException(string firstName, string lastName, string email, string password)
    {
        // Arrange
        var userCreateDto = new UserCreateDto
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        // Act & Assert
        Assert.Throws<ValidationException>(() => _validationService.Validate(userCreateDto));
    }

    [Test]
    public void Validate_WhenUserCreateDtoIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        UserCreateDto? userCreateDto = null;

        // Act
        TestDelegate action = () => _validationService.Validate(userCreateDto);

        // Assert
        var exception = Assert.Throws<ArgumentNullException>(action);

        Assert.Multiple(() =>
        {
            Assert.That(exception.ParamName, Is.EqualTo("dto"));
            Assert.That(exception.Message, Does.Contain("Value cannot be null."));
        });
    }
}