using System.Linq.Expressions;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Specifications;

public class UserWithEmailSpecification : Specification<User>
{
    private readonly string _emailPattern;

    public UserWithEmailSpecification(string emailPattern)
    {
        _emailPattern = emailPattern ?? throw new ArgumentNullException(nameof(emailPattern));
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Email.Value.Contains(_emailPattern, StringComparison.OrdinalIgnoreCase);
    }
}