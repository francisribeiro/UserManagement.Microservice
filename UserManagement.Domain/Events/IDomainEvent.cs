namespace UserManagement.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}