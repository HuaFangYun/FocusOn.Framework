using System.Linq;
using System.Xml.Linq;

using FocusOn.Framework.Business.Contract.Identity.DTO;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;

namespace FocusOn.Framework.Endpoint.HttpApi.UnitTest
{
    public class UserTest:TestBase
    {
        private readonly ITestUserBusinessService _userService;

        public UserTest()
        {
            _userService = GetRequiredService<ITestUserBusinessService>();
        }
        [Fact]
        public async Task TestCreateUser()
        {
            var result = await _userService.CreateAsync(new Test.WebHost.BusinessServices.Entities.User
            {
                UserName = "admin"+new Random().Next(9999),
                Name="aaa"
            });

            Assert.True(result.Succeed);
            Console.WriteLine(result.Errors.JoinString(";"));
        }
        [Fact]
        public async Task TestUpdateUser()
        {
            var user = new Test.WebHost.BusinessServices.Entities.User
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            };
            var result = await _userService.CreateAsync(user);

            Assert.True(result.Succeed);

            var updateResult = await _userService.UpdateAsync(user.Id, new Test.WebHost.BusinessServices.Entities.User
            {
                UserName= result.Data.UserName,
                Name = "bbb"+new Random().Next(999)
            });
            Assert.True(updateResult.Succeed);

            Assert.NotEqual(result.Data.Name, updateResult.Data.Name);
        }

        [Fact]
        public async Task TestDeleteUser()
        {
            var user = new Test.WebHost.BusinessServices.Entities.User
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            };
            var result = await _userService.CreateAsync(user);

            Assert.True(result.Succeed);

            var delete = await _userService.DeleteAsync(result.Data.Id);
            Assert.True(delete.Succeed);

            var find=await _userService.GetAsync(delete.Data.Id);
            Assert.False(find.Succeed);
        }

        [Fact]
        public async Task TestGetDetail()
        {
            var user = new Test.WebHost.BusinessServices.Entities.User
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            };
            var result = await _userService.CreateAsync(user);


            var model = await _userService.GetAsync(result.Data.Id);

            Assert.True(model.Succeed);

            Assert.Equal(user.UserName, model.Data.UserName);
        }

        [Fact]
        public async Task TestGetList()
        {
            Dispose();
            await _userService.CreateAsync(new Test.WebHost.BusinessServices.Entities.User
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            });

            await _userService.CreateAsync(new Test.WebHost.BusinessServices.Entities.User
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            });

            await _userService.CreateAsync(new Test.WebHost.BusinessServices.Entities.User
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            });

            var result= await _userService.GetListAsync();

            Assert.True(result.Succeed);

            Assert.NotEmpty(result.Data.Items);
            Assert.Equal(3, result.Data.Total);
        }
    }
}