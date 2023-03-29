namespace UserManagement.Application.Contracts;

public interface IValidationService
{
    void Validate<T>(T dto);
}