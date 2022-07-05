﻿using FocusOn.Framework.Business.Contract.DTO;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;
using FocusOn.Framework.Endpoint.HttpProxy;
using FocusOn.Framework.Endpoint.HttpProxy.Identity;

namespace FocusOn.Framework.Endpoint.HttpProxy.Test.Host.Proxies;

public class UserHttpApiClientProxy : IdentityUserCrudHttpApiClientProxy<Guid, User>, ITestUserBusinessService
{
    public UserHttpApiClientProxy(IServiceProvider services) : base(services)
    {
    }
}
