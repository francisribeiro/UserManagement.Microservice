using System.ComponentModel.DataAnnotations;
using UserManagement.Application.Contracts;

namespace UserManagement.Application.Services;

public class ValidationService : IValidationService
{
    public void Validate<T>(T dto)
    {
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        if (!isValid)
            throw new ValidationException(results.First().ErrorMessage);
    }
}