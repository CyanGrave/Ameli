using DAL.SQLite;
using AmeliAPI.UserManagement.DAL;
using Microsoft.EntityFrameworkCore.Design;


namespace AmeliAPI.UserManagement.DAL.SQLite
{
    public class ContextFactory : SQLiteContextFactory<MigrationContext>
    {
        
    }
}