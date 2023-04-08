using AutoMapper;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Exceptions;
using UserManagement.Application.Contracts;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Pagination;
using UserManagement.Application.DTOs.Requests;
using UserManagement.Application.Exceptions.User;

namespace UserManagement.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IValidationService _validationService;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IValidationService validationService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _validationService = validationService;
    }

    #region User management

    public async Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        _validationService.Validate(userCreateDto);

        await CheckEmailExistsAsync(userCreateDto.Email);

        var newUser = new User(
            userCreateDto.FirstName,
            userCreateDto.LastName,
            userCreateDto.Email,
            userCreateDto.Password);

        await _userRepository.CreateAsync(newUser);

        return _mapper.Map<UserDto>(newUser);
    }

    public async Task UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
    {
        _validationService.Validate(userUpdateDto);

        var user = await _userRepository.GetByIdAsync(id) ?? throw new UserNotFoundException(id);

        if (user.Email.Value != userUpdateDto.Email)
            await CheckEmailExistsAsync(userUpdateDto.Email);

        user.UpdateUser(userUpdateDto.FirstName, userUpdateDto.LastName, userUpdateDto.Email);

        await _userRepository.UpdateAsync(user);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return user == null ? throw new UserNotFoundException(id) : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        return user == null ? throw new EmailNotFoundException(email) : _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new UserNotFoundException(id);

        await _userRepository.DeleteAsync(user);
    }

    #endregion

    #region Password management

    public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId);

        if (!user.Password.Verify(currentPassword))
            throw new InvalidPasswordException();

        user.ChangePassword(currentPassword, newPassword);

        await _userRepository.UpdateAsync(user);
    }

    public async Task ResetPasswordAsync(Guid userId, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId);

        user.ResetPassword(newPassword);

        await _userRepository.UpdateAsync(user);
    }

    #endregion

    #region Role management

    public async Task AssignRoleAsync(Guid userId, UserRoleType roleType)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId);

        user.AssignRole(new Role(roleType));

        await _userRepository.UpdateAsync(user);
    }

    public async Task RemoveRoleAsync(Guid userId, UserRoleType roleType)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId);

        user.RemoveRole(new Role(roleType));

        await _userRepository.UpdateAsync(user);
    }

    #endregion

    #region User list and pagination

    public async Task<IEnumerable<UserDto>> GetUsersListAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<PagedResult<UserDto>> GetUsersListPagedAsync(int pageNumber, int pageSize)
    {
        var pagedUsers = await _userRepository.GetPagedUsersAsync(pageNumber, pageSize);

        return new PagedResult<UserDto>
        {
            PageNumber = pagedUsers.PageNumber,
            PageSize = pagedUsers.PageSize,
            TotalPages = pagedUsers.TotalPages,
            TotalRecords = pagedUsers.TotalRecords,
            Data = pagedUsers.Data.Select(user => new UserDto(user)).ToList()
        };
    }

    #endregion

    #region Authentication

    public async Task<UserDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        _validationService.Validate(loginRequestDto);

        var user = await _userRepository.FindByEmailAsync(loginRequestDto.Email);

        if (user != null && !user.Password.Verify(loginRequestDto.Password))
            throw new InvalidCredentialsException();

        user?.UpdateLoginDate();

        await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserDto>(user);
    }

    #endregion

    #region Utility

    public async Task CheckEmailExistsAsync(string email)
    {
        var existingUser = await _userRepository.FindByEmailAsync(email);

        if (existingUser != null)
            throw new EmailAlreadyExistsException(email);
    }

    #endregion
}