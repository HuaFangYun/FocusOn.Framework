using FocusOn.Framework.Business.Services;
using FocusOn.Framework.Business.Contract.Identity.DTO;

namespace FocusOn.Framework.IntegrationTest.Service;
public class MapProfile : CrudMapProfile<User, IdentityUserDetailOutput<Guid>, IdentityUserListOutput, IdentityUserCreateInput>
{
}
