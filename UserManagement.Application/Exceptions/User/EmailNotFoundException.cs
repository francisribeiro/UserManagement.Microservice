namespace UserManagement.Application.Exceptions.User;

public class EmailNotFoundException : ApplicationException
{
    public EmailNotFoundException(string email)
        : base($"An account with email '{email}' was not found.")
    {
    }
}