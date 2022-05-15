using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;
using AmeliAPI.Movies.Model.Entities;

namespace AmeliAPI.Movies.DAL.SQLite
{
    public class MigrationContext : MoviesContext
    {
        public MigrationContext() : base() { }

        public MigrationContext(DbContextOptions options) : base(options) { }
    }
}
