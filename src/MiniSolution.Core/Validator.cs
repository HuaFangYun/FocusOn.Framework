using System.ComponentModel.DataAnnotations;

namespace MiniSolution;

public class Validator
{
    public static bool TryValidate(object model,out IEnumerable<string?> errors)
    {
        var validationResult = new List<ValidationResult>();
        if(System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model,new ValidationContext(model), validationResult))
        {
            errors = Array.Empty<string>();
            return true;
        }

        errors=validationResult.Select(x => x.ErrorMessage);

        return false;
    }
}