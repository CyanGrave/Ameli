using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmeliAPI.UserManagement.Model.Entities;
using AmeliAPI.UserManagement.Model.Interfaces;
using ModelBase;

namespace LambdaSerializer
{
    public static class UserServiceExtensions
    {
        public static async Task ChangePasswordAsync<T>
            (this ICRUDServiceBase<User> service,string pwHash, string pwHashRpeat, params object[] keyValues)
                where T : User
        {
            var user = await service.GetByIDAsync(keyValues: keyValues);

        }
    }
}
