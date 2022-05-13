using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace DAL.SQLite
{
    internal class SQLiteOptionsBuilder : DAL.OptionsBuilder
    {


        public override DbContextOptions GetOptions<TContext>(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlite(string.IsNullOrEmpty(connectionString)? typeof(TContext).Name + ".db" : connectionString );

            return optionsBuilder.Options;
        }
    }
}