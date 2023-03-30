using System.Linq.Expressions;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Specifications;

public class UserHasRoleSpecification : Specification<User>
{
    private readonly UserRoleType _roleType;

    public UserHasRoleSpecification(UserRoleType roleType)
    {
        _roleType = roleType;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Roles.Any(r => r.Type == _roleType);
    }
}