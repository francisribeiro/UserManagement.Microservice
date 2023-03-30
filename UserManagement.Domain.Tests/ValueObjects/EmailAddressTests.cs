using NUnit.Framework;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.Tests.ValueObjects;

public class EmailAddressTests
{
    [Test]
    public void Constructor_ValidEmail_CreatesEmailAddress()
    {
        // Arrange
        const string email = "test@example.com";

        // Act
        var emailAddress = new EmailAddress(email);

        // Assert
        Assert.That(emailAddress.Value, Is.EqualTo(email));
    }

    [Test]
    public void Equals_SameEmail_ReturnsTrue()
    {
        // Arrange
        var email1 = new EmailAddress("test@example.com");
        var email2 = new EmailAddress("test@example.com");

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(email2, Is.EqualTo(email1));
            Assert.That(email1, Is.EqualTo(email2));
        });
    }

    [Test]
    public void Equals_DifferentEmail_ReturnsFalse()
    {
        // Arrange
        var email1 = new EmailAddress("test1@example.com");
        var email2 = new EmailAddress("test2@example.com");

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(email2, Is.Not.EqualTo(email1));
            Assert.That(email1, Is.Not.EqualTo(email2));
        });
    }

    [TestCase("test@example.com")]
    [TestCase("user.name@example.co.uk")]
    [TestCase("name+tag@example.io")]
    public void Constructor_ValidEmail_CreatesEmailAddress(string email)
    {
        // Act
        var emailAddress = new EmailAddress(email);

        // Assert
        Assert.That(emailAddress.Value, Is.EqualTo(email));
    }

    [TestCase("")]
    [TestCase(null)]
    [TestCase("invalidemail")]
    [TestCase("missingatsign.com")]
    [TestCase("missingdomain@example")]
    public void Constructor_InvalidEmail_ThrowsException(string email)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var emailAddress = new EmailAddress(email);
        });
    }

    [Test]
    public void GetHashCode_SameEmail_ReturnsSameHashCode()
    {
        // Arrange
        var email1 = new EmailAddress("test@example.com");
        var email2 = new EmailAddress("test@example.com");

        // Act
        var hashCode1 = email1.GetHashCode();
        var hashCode2 = email2.GetHashCode();

        // Assert
        Assert.That(hashCode2, Is.EqualTo(hashCode1));
    }

    [Test]
    public void GetHashCode_DifferentEmail_ReturnsDifferentHashCode()
    {
        // Arrange
        var email1 = new EmailAddress("test1@example.com");
        var email2 = new EmailAddress("test2@example.com");

        // Act
        var hashCode1 = email1.GetHashCode();
        var hashCode2 = email2.GetHashCode();

        // Assert
        Assert.That(hashCode2, Is.Not.EqualTo(hashCode1));
    }
}