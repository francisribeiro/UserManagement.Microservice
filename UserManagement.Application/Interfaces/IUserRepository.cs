using UserManagement.Domain.Entities;
using UserManagement.Domain.Specifications;
using UserManagement.Application.Pagination;

namespace UserManagement.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User?>> GetAllAsync();
    Task<PagedResult<User>> GetPagedUsersAsync(int pageNumber, int pageSize);
    Task<IEnumerable<User?>> FindAsync(Specification<User?> specification);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}