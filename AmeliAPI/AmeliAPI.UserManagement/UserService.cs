using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmeliAPI.UserManagement.Model.Entities;
using AmeliAPI.UserManagement.Model.Interfaces;
using ModelBase;

namespace AmeliAPI.UserManagement
{
    public class UserService : CRUDServiceBase<User>, IUserService
    {
        public UserService(IDataAccessProvider dataAccessProvider) : base(dataAccessProvider)
        {
        }
    }
}
