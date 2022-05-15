using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmeliAPI.Movies.Model.Entities;
using AmeliAPI.Movies.Model.Interfaces;
using ModelBase;

namespace AmeliAPI.Movies.Services
{
    public class MovieService : CRUDServiceBase<Movie>, IMovieService
    {
        public MovieService(IDataAccessProvider dataAccessProvider) : base(dataAccessProvider)
        {
        }
    }
}
