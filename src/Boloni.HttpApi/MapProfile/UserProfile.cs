using AutoMapper;

using Boloni.Data.Entities;
using Boloni.DataTransfers.Users;

namespace Boloni.HttpApi.MapProfile
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserInputDto, User>();
        }
    }
}
