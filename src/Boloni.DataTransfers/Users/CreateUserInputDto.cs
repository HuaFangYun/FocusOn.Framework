namespace Boloni.DataTransfers.Users;

public class CreateUserInputDto
{
    [Display(Name =nameof(Locale.Display_User_UserName),ResourceType =typeof(Locale))]
    [Required(ErrorMessageResourceType =typeof(Locale),ErrorMessageResourceName =nameof(Locale.Message_FieldIsRequired))]
    public string UserName { get; set; }

    [Display(Name = nameof(Locale.Display_User_Password), ResourceType = typeof(Locale))]
    [Required(ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = nameof(Locale.Message_FieldIsRequired))]
    public string Password { get; set; }

    [Display(Name = nameof(Locale.Display_User_Mobile), ResourceType = typeof(Locale))]
    public string? Mobile { get; set; }

    [Display(Name = nameof(Locale.Display_User_Email), ResourceType = typeof(Locale))]
    public string? Email { get; set; }


    [Display(Name = nameof(Locale.Display_User_Name), ResourceType = typeof(Locale))]
    public string? Name { get; set; }
}
