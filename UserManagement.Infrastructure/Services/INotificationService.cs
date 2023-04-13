namespace UserManagement.Application.Services;

public interface INotificationService
{
    Task SendNotificationAsync(string userId, string message);
}