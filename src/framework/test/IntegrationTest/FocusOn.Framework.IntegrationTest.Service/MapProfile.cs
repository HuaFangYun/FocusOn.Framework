using FocusOn.Framework.Business.Service;
using FocusOn.Framework.Business.Contract.Identity.DTO;

namespace FocusOn.Framework.IntegrationTest.Service;
public class MapProfile : CrudMapProfile<User, IdentityUserDetailOutput<Guid>, IdentityUserListOutput, IdentityUserCreateInput>
{
}

public class RoleMap : CrudMapProfile<Role, IdentityRoleDetailOutput, IdentityRoleListOutput, IdentityRoleCreateInput>
{

}
