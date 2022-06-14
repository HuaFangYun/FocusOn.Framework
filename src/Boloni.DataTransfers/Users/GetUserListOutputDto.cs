namespace Boloni.DataTransfers.Users;

public class GetUserListOutputDto
{
    [Display(Name = nameof(Locale.Display_User_UserName), ResourceType = typeof(Locale))]
    public string UserName { get; set; }

    [Display(Name = nameof(Locale.Display_User_Mobile), ResourceType = typeof(Locale))]
    public string? Mobile { get; set; }

    [Display(Name = nameof(Locale.Display_User_Email), ResourceType = typeof(Locale))]
    public string? Email { get; set; }

    [Display(Name = nameof(Locale.Display_User_Name), ResourceType = typeof(Locale))]
    public string? Name { get; set; }
}
