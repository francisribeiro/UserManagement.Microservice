using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.DTOs;

public static class DtoValidator
{
    public static void Validate<T>(T dto)
    {
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        if (!isValid)
            throw new ValidationException(results.First().ErrorMessage);
    }
}

