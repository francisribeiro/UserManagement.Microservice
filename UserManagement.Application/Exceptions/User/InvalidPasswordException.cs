namespace UserManagement.Application.Exceptions.User;

public class InvalidPasswordException : ApplicationException
{
    public InvalidPasswordException()
        : base("Invalid password.")
    {
    }
}