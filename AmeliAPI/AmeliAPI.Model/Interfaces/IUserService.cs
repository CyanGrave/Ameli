using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelBase;
using AmeliAPI.UserManagement.Model.Entities;

namespace AmeliAPI.UserManagement.Model.Interfaces
{
    public interface IUserService : ICRUDServiceBase<User>
    {
    }
}
