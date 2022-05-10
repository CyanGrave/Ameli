using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;
using AmeliAPI.UserManagement.Model.Entities;

namespace AmeliAPI.UserManagement.DAL
{
    public class UserManagementContext : Context
    {
        public override string SchemaName => "UserManagement";

        public UserManagementContext() : base() { }

        public UserManagementContext(DbContextOptions options) : base(options) { }



        public virtual DbSet<User> CPE { get; set; }

    }
}
