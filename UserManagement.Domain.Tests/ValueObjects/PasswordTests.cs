using NUnit.Framework;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.Tests.ValueObjects;

public class PasswordTests
{
    [Test]
    public void CreateFromPlainText_ValidPassword_CreatesPassword()
    {
        // Arrange
        const string plainTextPassword = "ValidPassword123";

        // Act
        var password = Password.CreateFromPlainText(plainTextPassword);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(password, Is.Not.Null);
            Assert.That(password.HashedValue, Is.Not.Null);
            Assert.That(password.HashedValue, Is.Not.EqualTo(plainTextPassword));
        });
    }

    [Test]
    public void Verify_CorrectPassword_ReturnsTrue()
    {
        // Arrange
        const string plainTextPassword = "ValidPassword123";
        var password = Password.CreateFromPlainText(plainTextPassword);

        // Act
        var isVerified = password.Verify(plainTextPassword);

        // Assert
        Assert.That(isVerified, Is.True);
    }

    [Test]
    public void Verify_IncorrectPassword_ReturnsFalse()
    {
        // Arrange
        var plainTextPassword = "ValidPassword123";
        var wrongPassword = "WrongPassword456";
        var password = Password.CreateFromPlainText(plainTextPassword);

        // Act
        var isVerified = password.Verify(wrongPassword);

        // Assert
        Assert.That(isVerified, Is.False);
    }


    [TestCase("")]
    [TestCase("short")]
    [TestCase("1234567890")]
    [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    public void CreateFromPlainText_InvalidPassword_ThrowsException(string plainTextPassword)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Password.CreateFromPlainText(plainTextPassword));
    }

    [Test]
    public void Equals_SamePassword_ReturnsTrue()
    {
        // Arrange
        const string plainTextPassword = "ValidPassword123";
        var password1 = Password.CreateFromPlainText(plainTextPassword);
        var password2 = Password.CreateFromPlainText(plainTextPassword);

        // Act
        var arePasswordsEqual = password1.EqualTo(plainTextPassword) && password2.EqualTo(plainTextPassword);

        // Assert
        Assert.That(arePasswordsEqual, Is.True);
    }

    [Test]
    public void Equals_DifferentPassword_ReturnsFalse()
    {
        // Arrange
        const string plainTextPassword1 = "ValidPassword123";
        const string plainTextPassword2 = "DifferentPassword456";
        var password1 = Password.CreateFromPlainText(plainTextPassword1);
        var password2 = Password.CreateFromPlainText(plainTextPassword2);

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(password2, Is.Not.EqualTo(password1));
            Assert.That(password1, Is.Not.EqualTo(password2));
        });
    }

    [Test]
    public void GetHashCode_SamePassword_ReturnsSameHashCode()
    {
        // Arrange
        const string plainTextPassword = "ValidPassword123";
        var password = Password.CreateFromPlainText(plainTextPassword);

        // Act
        var hashCode1 = password.GetHashCode();
        var hashCode2 = password.GetHashCode();

        // Assert
        Assert.That(hashCode2, Is.EqualTo(hashCode1));
    }


    [Test]
    public void GetHashCode_DifferentPassword_ReturnsDifferentHashCode()
    {
        // Arrange
        const string plainTextPassword1 = "ValidPassword123";
        const string plainTextPassword2 = "DifferentPassword456";
        var password1 = Password.CreateFromPlainText(plainTextPassword1);
        var password2 = Password.CreateFromPlainText(plainTextPassword2);

        // Act
        var hashCode1 = password1.GetHashCode();
        var hashCode2 = password2.GetHashCode();

        // Assert
        Assert.That(hashCode2, Is.Not.EqualTo(hashCode1));
    }
}