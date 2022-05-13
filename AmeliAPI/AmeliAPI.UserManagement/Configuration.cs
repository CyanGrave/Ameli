using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AmeliAPI.UserManagement.Model.Entities;
using AmeliAPI.UserManagement.Model.Interfaces;
using AmeliAPI.UserManagement.Services;
using AmeliAPI.UserManagement.DAL;
using ModelBase;
using DAL;

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
