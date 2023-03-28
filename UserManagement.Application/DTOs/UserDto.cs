using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public ICollection<UserRole> Roles { get; set; }
    
    public UserDto(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email.Value;
        CreatedAt = user.CreatedAt;
        LastLogin = user.LastLogin;
        Roles = user.UserRoles; 
    }
}