using UserManagement.Application.Contracts;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserDTO> CreateUserAsync(UserCreateDTO userCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AssignRoleAsync(Guid userId, UserRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRoleAsync(Guid userId, UserRoleType roleType)
        {
            throw new NotImplementedException();
        }

        public Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(Guid userId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}