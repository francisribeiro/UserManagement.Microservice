using UserManagement.Application.Contracts;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.Services;

public class ValidationService : IValidationService
{
    public void Validate<T>(T dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "Value cannot be null.");

        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        if (!isValid)
            throw new ValidationException(results.First().ErrorMessage);
    }
}