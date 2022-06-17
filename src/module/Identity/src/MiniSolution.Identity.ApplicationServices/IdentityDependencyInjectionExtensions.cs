using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MiniSolution.Identity.ApplicationContracts.UserManagement;
using MiniSolution.Identity.ApplicationContracts.UserManagement.DTO;
using MiniSolution.Identity.ApplicationServices.UserManagement;
using MiniSolution.Identity.ApplicationServices.UserManagement.Entities;

namespace MiniSolution.Identity.ApplicationServices;

public static class IdentityDependencyInjectionExtensions
{
    public static MiniSolutionBuilder AddIdentityUser<TApplicationService, TApplicationImplementation, TUser, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>(this MiniSolutionBuilder builder)
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
