namespace UserManagement.Application.Exceptions;

public class InvalidPasswordException : ApplicationException
{
    public InvalidPasswordException()
        : base("Invalid password.")
    {
    }
}