﻿using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Business.Contract.Identity;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;

public interface ITestUserBusinessService : IIdentityUserCrudBusinessService<Guid, User>
{
}
