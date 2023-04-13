using UserManagement.Application.Services;

namespace UserManagement.Infrastructure.Services;

public class NotificationService : INotificationService
{
    public async Task SendNotificationAsync(string userId, string message)
    {
        // Implement the actual notification sending logic here, e.g., by pushing a message to a message broker, sending an SMS, or any other method
        await Task.CompletedTask;
    }
}