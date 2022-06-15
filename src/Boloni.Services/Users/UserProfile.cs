using AutoMapper;

using Boloni.Data.Entities;
using Boloni.DataTransfers.Users;
using Boloni.Services.Abstractions;

namespace Boloni.Services.Users
{
    public class UserProfile : CrudProfile<User,GetUserOutputDto,GetUserListOutputDto,CreateUserInputDto,UpdateUserInputDto>
    {
        public UserProfile()
        {
        }
    }
}
