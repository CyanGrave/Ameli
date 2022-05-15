using AmeliAPI.UserManagement.Model.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace AmeliAPI.UserManagement.DAL
{
    public class UserManagementContext : Context
    {
        public override string SchemaName => "UserManagement";

        public UserManagementContext() : base() { }

        public UserManagementContext(DbContextOptions options) : base(options) { }



        public virtual DbSet<User> User { get; set; }

    }
}
