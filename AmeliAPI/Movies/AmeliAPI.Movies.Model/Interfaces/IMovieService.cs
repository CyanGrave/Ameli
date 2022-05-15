using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelBase;
using AmeliAPI.Movies.Model.Entities;

namespace AmeliAPI.Movies.Model.Interfaces
{
    public interface IMovieService : ICRUDServiceBase<Movie>
    {
    }
}
