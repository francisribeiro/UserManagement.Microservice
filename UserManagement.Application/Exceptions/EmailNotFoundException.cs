namespace UserManagement.Application.Exceptions;

public class EmailNotFoundException : ApplicationException
{
    public EmailNotFoundException(string email)
        : base($"An account with email '{email}' was not found.")
    {
    }
}