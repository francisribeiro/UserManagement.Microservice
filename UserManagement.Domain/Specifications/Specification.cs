using System.Linq.Expressions;

namespace UserManagement.Domain.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();
}