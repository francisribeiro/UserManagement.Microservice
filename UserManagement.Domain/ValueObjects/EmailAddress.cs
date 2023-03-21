using System.Text.RegularExpressions;

namespace UserManagement.Domain.ValueObjects;

public class EmailAddress : IEquatable<EmailAddress>
{
    public string Value { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValid(value))
            throw new ArgumentException("Invalid email address", nameof(value));

        Value = value;
    }

    private bool IsValid(string email)
    {
        // Use a simple regular expression or a more robust validation library
        return Regex.IsMatch(email, @"^\S+@\S+\.\S+$");
    }

    public bool Equals(EmailAddress other)
    {
        if (other == null) return false;
        return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }
    
    public string GetDomain()
    {
        int atIndex = Value.IndexOf('@');
        return Value.Substring(atIndex + 1);
    }

    public override bool Equals(object obj) => Equals(obj as EmailAddress);
    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    public override string ToString() => Value;
}