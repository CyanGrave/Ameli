using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace DAL.SQLite
{
    internal class OptionsBuilder : DAL.OptionsBuilder
    {

        public override DbContextOptions GetOptions<TContext>(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlite();

            return optionsBuilder.Options;
        }
    }
}