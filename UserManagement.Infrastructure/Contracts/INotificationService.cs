namespace UserManagement.Infrastructure.Contracts;

public interface INotificationService
{
    Task SendNotificationAsync(string userId, string message);
}