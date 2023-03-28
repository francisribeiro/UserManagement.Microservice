using UserManagement.Domain.Entities;
using UserManagement.Domain.Specifications;

namespace UserManagement.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> FindByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> FindAsync(Specification<User> specification);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}