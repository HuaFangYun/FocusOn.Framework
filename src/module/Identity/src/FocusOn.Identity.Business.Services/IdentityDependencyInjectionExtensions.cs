using Microsoft.Extensions.DependencyInjection;

using FocusOn.Identity.Business.Contracts.UserManagement;
using FocusOn.Identity.Business.Services.UserManagement.Entities;

namespace FocusOn.Identity.Business.Services;

public static class IdentityDependencyInjectionExtensions
{
    public static FocusOnBuilder AddIdentityUser<TBusinessService, TApplicationImplementation, TUser, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>(this FocusOnBuilder builder)
        where TBusinessService : class, IUserBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
        where TApplicationImplementation : class, TBusinessService
        where TUser : IdentityUser<TKey>
        where TGetListInput : class
        where TGetListOutput : class
        where TGetOutput : class
        where TCreateInput : class
        where TUpdateInput : class
    {
        builder.Services.AddScoped<TBusinessService, TApplicationImplementation>();
        return builder;
    }
}
