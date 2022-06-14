using AutoMapper;

using Boloni.Data.Entities;
using Boloni.DataTransfers.Users;

namespace Boloni.HttpApi.Users
{
    public class UserProfile : CrudProfile<User,GetUserOutputDto,GetUserListOutputDto,CreateUserInputDto,UpdateUserInputDto>
    {
        public UserProfile()
        {
        }
    }
}
