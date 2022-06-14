

namespace Boloni.DataTransfers.Users;

public class UpdateUserInputDto
{
    [Display(Name = nameof(Locale.Display_User_Name), ResourceType = typeof(Locale))]
    public string? Name { get; set; }

}
