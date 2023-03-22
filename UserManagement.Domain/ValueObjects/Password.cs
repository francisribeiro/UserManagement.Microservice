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

        if (!IsValidPassword(plainText))
            throw new ArgumentException("Invalid password", nameof(plainText));

        // Generate a random salt
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var hashedValue = Hash(plainText, salt);
        var saltedHashedValue = $"{Convert.ToBase64String(salt)}:{hashedValue}";

        return new Password(saltedHashedValue);
    }

    private static string Hash(string plainText, byte[] salt)
    {
        // Hash the plainText password with the salt
        var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: plainText,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hashedPassword;
    }

    public bool Verify(string plainText)
    {
        var saltHashParts = HashedValue.Split(':');
        var salt = Convert.FromBase64String(saltHashParts[0]);
        var hashedPlainText = Hash(plainText, salt);

        return saltHashParts[1] == hashedPlainText;
    }

    private static bool IsValidPassword(string plainText)
    {
        const int minLength = 8;
        var hasLetters = plainText.Any(char.IsLetter);
        var hasDigits = plainText.Any(char.IsDigit);

        return plainText.Length >= minLength && hasLetters && hasDigits;
    }

    public bool EqualsPlainText(string plainText)
    {
        var saltAndPassword = HashedValue.Split(':');
        var salt = Convert.FromBase64String(saltAndPassword[0]);
        var hashedValue = Hash(plainText, salt);

        return saltAndPassword[1] == hashedValue;
    }
}