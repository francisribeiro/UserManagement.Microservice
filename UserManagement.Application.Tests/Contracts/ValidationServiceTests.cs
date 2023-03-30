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
    public void Validate_ValidUserCreateDto_DoesNotThrowException()
    {
        var userCreateDto = new UserCreateDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "password123"
        };

        Assert.DoesNotThrow(() => _validationService.Validate(userCreateDto));
    }

    [Test]
    public void Validate_InvalidUserCreateDto_ThrowsValidationException()
    {
        var userCreateDto = new UserCreateDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "invalid_email",
            Password = "short"
        };

        Assert.Throws<ValidationException>(() => _validationService.Validate(userCreateDto));
    }

    [Test]
    public void Validate_NullUserCreateDto_ThrowsArgumentNullException()
    {
        UserCreateDto? userCreateDto = null;

        Assert.Throws<ArgumentNullException>(() => _validationService.Validate(userCreateDto));
    }
}
