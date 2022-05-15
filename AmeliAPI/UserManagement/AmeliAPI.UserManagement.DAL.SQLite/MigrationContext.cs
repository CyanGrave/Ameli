using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;
using AmeliAPI.UserManagement.Model.Entities;

namespace AmeliAPI.UserManagement.DAL.SQLite
{
    public class MigrationContext : UserManagementContext
    {
        public MigrationContext() : base() { }

        public MigrationContext(DbContextOptions options) : base(options) { }
    }
}
