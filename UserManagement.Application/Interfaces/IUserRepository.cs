using UserManagement.Domain.Entities;
using UserManagement.Domain.Specifications;

namespace UserManagement.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetByIdAsync(Guid id);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<IEnumerable<User>> FindAsync(Specification<User> specification);
}