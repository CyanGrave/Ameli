using AmeliAPI.UserManagement.Model.Interfaces;
using AmeliAPI.UserManagement.Services;
using Microsoft.Extensions.DependencyInjection;
using ModelBase;

namespace AmeliAPI.UserManagement
{
    public static class Configuration
    {
        public static void ConfigureServices(IServiceCollection services, IDataAccessProvider dataAccessProvider)
        {
            services.AddSingleton<IUserService>( new UserService(dataAccessProvider));
        }
    }
}
