using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Contracts;

public interface IAuthenticationService
{
    string GenerateJwtToken(User user);
}