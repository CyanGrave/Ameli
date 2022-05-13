using DAL;
using Microsoft.EntityFrameworkCore.Design;


namespace DAL
{
    public abstract class ContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>  where TContext : Context, new()
    {

        public abstract OptionsBuilder GetOptionsBuilder();

        public TContext CreateDbContext(string[]? args = null)
        {
            var optionsBuilder = GetOptionsBuilder();

            if(optionsBuilder is null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            var context = Activator.CreateInstance(typeof(TContext), new object[] { optionsBuilder.GetOptions<TContext>(string.Empty) }) as TContext;

            if (context is null)
                throw new Exception("Context Creation failed");

            return context;
        }
    }
}