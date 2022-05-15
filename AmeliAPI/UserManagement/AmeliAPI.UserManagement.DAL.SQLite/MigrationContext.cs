using Microsoft.EntityFrameworkCore;

namespace AmeliAPI.UserManagement.DAL.SQLite
{
    public class MigrationContext : UserManagementContext
    {
        public MigrationContext() : base() { }

        public MigrationContext(DbContextOptions options) : base(options) { }
    }
}
