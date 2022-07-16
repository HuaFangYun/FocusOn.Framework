using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

namespace FocusOn.Framework.Endpoint.HttpApi.UnitTest
{
    public class UserTest : TestBase
    {
        private readonly ITestUserBusinessService _userService;

        public UserTest()
        {
            _userService = GetRequiredService<ITestUserBusinessService>();
        }
        [Fact]
        public async Task TestCreateUser()
        {
            var model = new UserCreateInput
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            };
            var result = await _userService.CreateAsync(model);

            Assert.True(result.Succeed);
            Console.WriteLine(result.Errors.JoinString(";"));

            var getResult = await _userService.GetAsync(result.Data.Id);
            Assert.True(getResult.Succeed);

            Assert.Equal(model.UserName, getResult.Data.UserName);
            Assert.Equal(model.Name, getResult.Data.Name);
        }
        [Fact]
        public async Task TestUpdateUser()
        {
            var user = new UserCreateInput
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            };
            var result = await _userService.CreateAsync(user);

            Assert.True(result.Succeed);

            var updateResult = await _userService.UpdateAsync(result.Data.Id, new User
            {
                UserName = result.Data.UserName,
                Name = "bbb" + new Random().Next(999)
            });
            Assert.True(updateResult.Succeed);

            Assert.NotEqual(result.Data.Name, updateResult.Data.Name);
        }

        [Fact]
        public async Task TestDeleteUser()
        {
            var user = new UserCreateInput
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            };
            var result = await _userService.CreateAsync(user);

            Assert.True(result.Succeed);

            var delete = await _userService.DeleteAsync(result.Data.Id);
            Assert.True(delete.Succeed);

            var find = await _userService.GetAsync(delete.Data.Id);
            Assert.False(find.Succeed);
        }

        [Fact]
        public async Task TestGetDetail()
        {
            var user = new UserCreateInput
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
            await _userService.CreateAsync(new UserCreateInput
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            });

            await _userService.CreateAsync(new UserCreateInput
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            });

            await _userService.CreateAsync(new UserCreateInput
            {
                UserName = "admin" + new Random().Next(9999),
                Name = "aaa"
            });

            var result = await _userService.GetListAsync();

            Assert.True(result.Succeed);

            Assert.NotEmpty(result.Data.Items);
            Assert.Equal(3, result.Data.Total);
        }


        public async Task TestUserSearch_WithUserName()
        {
            await _userService.CreateAsync(new UserCreateInput
            {
                UserName = "admin",
                Name = "aaa"
            });

            await _userService.CreateAsync(new UserCreateInput
            {
                UserName = "张三",
                Name = "aaa"
            });

            await _userService.CreateAsync(new UserCreateInput
            {
                UserName = "李四",
                Name = "aaa"
            });

            var result = await _userService.GetListAsync(new User
            {
                UserName = "ad"
            });

            Assert.NotEmpty(result.Data.Items);
            Assert.Single(result.Data.Items);

            var data = result.Data.Items.First();

            Assert.Equal("admin", data.UserName);
        }
    }
}