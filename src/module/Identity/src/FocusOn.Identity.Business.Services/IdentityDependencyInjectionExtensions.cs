using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using FocusOn.Identity.Business.Contracts.UserManagement;
using FocusOn.Identity.Business.Contracts.UserManagement.DTO;
using FocusOn.Identity.Business.Services.UserManagement;
using FocusOn.Identity.Business.Services.UserManagement.Entities;

namespace FocusOn.Identity.Business.Services;

public static class IdentityDependencyInjectionExtensions
{
    public static FocusOnBuilder AddIdentityUser<TApplicationService, TApplicationImplementation, TUser, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>(this FocusOnBuilder builder)
        where TApplicationService : class, IUserApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
        where TApplicationImplementation : class, TApplicationService
        where TUser : User<TKey>
        where TGetListInput : UserGetListInputDto
        where TGetListOutput : UserGetListOutputDto<TKey>
        where TGetOutput : UserGetOutputDto<TKey>
        where TCreateInput : UserCreateInputDto
        where TUpdateInput : UserUpdateInputDto
    {
        builder.Services.AddScoped<TApplicationService, TApplicationImplementation>();
        return builder;
    }
}
