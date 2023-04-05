namespace UserManagement.Application.Exceptions.User;

public class EmailAlreadyExistsException : ApplicationException
{
    public EmailAlreadyExistsException(string email)
        : base($"An account with email '{email}' already exists.")
    {
    }
}