using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Business.Services;

namespace FocusOn.Framework.IntegrationTest.Service;
public class MapProfile : CrudMapProfile<User, IdentityUserDetailOutput, IdentityUserListOutput, IdentityUserCreateInput>
{
}
