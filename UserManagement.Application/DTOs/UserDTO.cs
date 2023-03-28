using UserManagement.Domain.Enums;

namespace UserManagement.Application.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public ICollection<UserRoleType> Roles { get; set; }
}