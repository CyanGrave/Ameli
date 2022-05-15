using AmeliAPI.Movies.Model.Interfaces;
using AmeliAPI.Movies.Services;
using Microsoft.Extensions.DependencyInjection;
using ModelBase;

namespace AmeliAPI.Movies
{
    public static class Configuration
    {
        public static void ConfigureServices(IServiceCollection services, IDataAccessProvider dataAccessProvider)
        {
            services.AddSingleton<IMovieService>( new MovieService(dataAccessProvider));
        }
    }
}
