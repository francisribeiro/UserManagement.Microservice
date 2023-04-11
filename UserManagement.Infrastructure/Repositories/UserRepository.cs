using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Specifications;
using UserManagement.Application.Pagination;
using UserManagement.Application.Interfaces;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagementDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserRepository(UserManagementDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email.Value == email);
    }

    public async Task<IEnumerable<User?>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<PagedResult<User>> GetPagedUsersAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        var totalRecords = await _dbContext.Users.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        var users = await _dbContext.Users
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var userDtos = _mapper.Map<List<User>>(users);

        return new PagedResult<User>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalRecords = totalRecords,
            Data = userDtos
        };
    }

    public async Task<IEnumerable<User?>> FindAsync(Specification<User?> specification)
    {
        return await _dbContext.Users.Where(specification.ToExpression()).ToListAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}