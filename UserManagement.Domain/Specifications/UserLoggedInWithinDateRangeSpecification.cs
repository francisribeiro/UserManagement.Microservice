using System.Linq.Expressions;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Specifications;

public class UserLoggedInWithinDateRangeSpecification : Specification<User>
{
    private readonly DateTime _startDate;
    private readonly DateTime _endDate;

    public UserLoggedInWithinDateRangeSpecification(DateTime startDate, DateTime endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user =>
            user.LastLogin.HasValue && user.LastLogin.Value >= _startDate && user.LastLogin.Value <= _endDate;
    }
}