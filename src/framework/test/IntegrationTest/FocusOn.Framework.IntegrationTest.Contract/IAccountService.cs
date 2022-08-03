using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using FocusOn.Framework.Business.Contract;
using FocusOn.Framework.Business.Contract.Http;

namespace FocusOn.Framework.IntegrationTest.Contract;
[Route("api/account")]
public interface IAccountService : IRemotingService
{
    [Post]
    Task<Return> SignInAsync(string account, string password);
}
