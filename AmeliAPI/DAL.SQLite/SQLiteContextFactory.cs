using DAL.SQLite;
using DAL;
using Microsoft.EntityFrameworkCore.Design;


namespace DAL.SQLite
{
    public class SQLiteContextFactory<TContext> : DAL.ContextFactory<TContext> where TContext : Context, new()
    {
        public override DAL.OptionsBuilder GetOptionsBuilder()
        {
            return new OptionsBuilder();
        }
    }
}