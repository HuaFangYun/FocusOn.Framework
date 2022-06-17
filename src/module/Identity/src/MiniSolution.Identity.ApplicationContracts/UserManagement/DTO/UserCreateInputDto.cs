using System.ComponentModel.DataAnnotations;

namespace MiniSolution.Identity.ApplicationContracts.UserManagement.DTO;

public class UserCreateInputDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
