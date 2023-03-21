using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace UserManagement.Domain.ValueObjects;

public class Password
{
    public string HashedValue { get; }

    private Password(string hashedValue)
    {
        HashedValue = hashedValue;
    }

    public static Password CreateFromPlainText(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            throw new ArgumentException("Password cannot be null or empty", nameof(plainText));

        // Use a secure hashing algorithm, e.g., \
        string hashedValue = Hash(plainText);
        return new Password(hashedValue);
    }

    private static string Hash(string plainText)
    {
        // Generate a random salt
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the plainText password with the salt
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: plainText,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hashedPassword;
    }

    public bool Verify(string plainText)
    {
        string hashedPlainText = Hash(plainText);
        return HashedValue == hashedPlainText;
    }
}