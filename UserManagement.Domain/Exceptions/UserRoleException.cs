namespace UserManagement.Domain.Exceptions;

public class UserRoleException : DomainException
{
    public UserRoleException(string message)
        : base(message)
    {
    }
}
