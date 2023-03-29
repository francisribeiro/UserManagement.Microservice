// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using UserManagement.Domain.Entities;
// using UserManagement.Application.Interfaces;
// using UserManagement.Application.Pagination;
//
// namespace UserManagement.Infrastructure.Repositories;
//
// public class UserRepository : IUserRepository
// {
//     // Other methods and constructor remain the same
//
//     public async Task<PagedResult<User>> GetPagedUsersAsync(int pageNumber, int pageSize)
//     {
//         var totalRecords = await _dbContext.Users.CountAsync();
//         var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
//         var data = await _dbContext.Users
//             .Skip((pageNumber - 1) * pageSize)
//             .Take(pageSize)
//             .ToListAsync();
//
//         return new PagedResult<User>
//         {
//             PageNumber = pageNumber,
//             PageSize = pageSize,
//             TotalPages = totalPages,
//             TotalRecords = totalRecords,
//             Data = data
//         };
//     }
// }